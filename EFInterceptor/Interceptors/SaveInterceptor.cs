using EFInterceptor.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;


namespace EFInterceptor.Interceptors
{
    /// <inheritdoc/>
    public class SaveInterceptor : SaveChangesInterceptor
    {

        /// <inheritdoc/>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
        
            if (eventData.Context is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var changes = eventData.Context.ChangeTracker.Entries().ToList();

            if (changes is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            foreach (var entry in changes)
            {
                //if not valid item, go next
                if (entry.Entity is not BaseItem item)
                    continue;

                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        //change state to modified then set delete date
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        item.DeleteDate = DateTime.Now;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        //set update time
                        item.LastUpdate = DateTime.Now;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        //set creation date
                        item.CreationDate = DateTime.Now;
                        break;
                    default:
                        break;
                }

                //Add Log Event
                eventData.Context.Add(new LogEvent()
                {
                    State = entry.State.ToString(),
                    Date = DateTime.Now,
                    Input = JsonSerializer.Serialize(entry.Entity),
                });

            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <inheritdoc />
        public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return base.SaveChangesFailedAsync(eventData, cancellationToken);

            var changes = eventData.Context.ChangeTracker.Entries().ToList();

            if (changes is null)
                return base.SaveChangesFailedAsync(eventData, cancellationToken);



            //clear all entries
            eventData.Context.ChangeTracker.Clear();


            foreach (var entry in changes)
            {
                //if not valid item, go next
                if (entry.Entity is not BaseItem)
                    continue;

                eventData.Context.Attach(new LogEvent()
                {
                    Date = DateTime.Now,
                    Input = JsonSerializer.Serialize(entry.Entity),
                    State = "ERROR - ",
                    Detail = eventData.Exception.InnerException?.Message
                });
            }

            eventData.Context.SaveChanges();

            return base.SaveChangesFailedAsync(eventData, cancellationToken);
        }
    }
}
