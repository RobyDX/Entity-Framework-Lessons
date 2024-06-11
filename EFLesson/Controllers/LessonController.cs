using EFLesson.Data;
using EFLesson.Dto.Lesson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EFLesson.Controllers
{
    /// <summary>
    /// Lesson Controller
    /// </summary>
    /// <param name="db"></param>
    [ApiController]
    [Route("[controller]")]
    public class LessonController(DB db) : ControllerBase
    {
        /// <summary>
        /// Select
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("Select")]
        [ProducesDefaultResponseType(typeof(List<UserOutput>))]
        public async Task<IActionResult> Select()
        {

            var result = await (from u in db.Users
                                select new UserOutput()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    BirthDate = u.BirthDate
                                }).ToListAsync();

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]

            */

            return Ok(result);
        }




        /// <summary>
        /// Select first
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>Result</returns>
        [HttpGet("SelectFirst/{id}")]
        [ProducesDefaultResponseType(typeof(UserOutput))]
        public async Task<IActionResult> SelectFirst([FromRoute] int id)
        {
            var result = await (from u in db.Users
                                where u.Id == id
                                select new UserOutput()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    BirthDate = u.BirthDate
                                }).FirstOrDefaultAsync();

            return Ok(result);

            /*
            QUERY

            SELECT TOP(1) [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            WHERE [u].[Id] = @__id_0

             */
        }


        /// <summary>
        /// Order
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("Order")]
        [ProducesDefaultResponseType(typeof(List<UserOutput>))]
        public async Task<IActionResult> Order()
        {

            var result = await (from u in db.Users
                                orderby u.Surname ascending, u.Name ascending
                                select new UserOutput()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    BirthDate = u.BirthDate
                                }).ToListAsync();

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            ORDER BY [u].[Surname], [u].[Name]

            */

            return Ok(result);
        }


        /// <summary>
        /// Select Any
        /// </summary>
        /// <param name="name">User Name</param>
        /// <returns>Result</returns>
        [HttpGet("Any/{name}")]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<IActionResult> Any([FromRoute] string name)
        {
            var result = await (from u in db.Users
                                where u.Name.Contains(name)
                                select u).AnyAsync();

            return Ok(result);

            /*
            QUERY

            SELECT CASE
                WHEN EXISTS (
                    SELECT 1
                    FROM [User] AS [u]
                    LEFT JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
                    WHERE [u].[Name] LIKE @__name_0_contains ESCAPE N'\') THEN CAST(1 AS bit)
                ELSE CAST(0 AS bit)
            END

             */
        }

        /// <summary>
        /// Select Any
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <returns>Result</returns>
        [HttpGet("All/{courseId}")]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<IActionResult> All([FromRoute] int courseId)
        {
            var result = await db.Exams.AllAsync(c => c.CourseId == courseId && c.Vote.HasValue);

            return Ok(result);

            /*
            QUERY

            SELECT CASE
                WHEN NOT EXISTS (
                    SELECT 1
                    FROM [Exam] AS [e]
                    WHERE [e].[CourseId] <> @__courseId_0 OR [e].[Vote] IS NULL) THEN CAST(1 AS bit)
                ELSE CAST(0 AS bit)
            END

             */
        }


        /// <summary>
        /// Select first with lambda syntax
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>Result</returns>
        [HttpGet("SelectFirstLambda/{id}")]
        [ProducesDefaultResponseType(typeof(UserOutput))]
        public async Task<IActionResult> SelectFirstLambda([FromRoute] int id)
        {
            var result = await db.Users
                .Where(u => u.Id == id)
                .Select(u => new UserOutput()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    BirthDate = u.BirthDate
                }).FirstOrDefaultAsync();

            return Ok(result);

            /*
            QUERY

            SELECT TOP(1) [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            WHERE [u].[Id] = @__id_0

             */
        }


        /// <summary>
        /// Select with pagination
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="limit">Limit</param>
        /// <returns>Result</returns>
        [HttpGet("Pagination")]
        [ProducesDefaultResponseType(typeof(List<UserOutput>))]
        public async Task<IActionResult> Pagination([FromQuery] int offset, [FromQuery] int limit)
        {
            var result = await (from u in db.Users
                                select new UserOutput()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    BirthDate = u.BirthDate
                                }

                                )
                                .OrderBy(x => x.Id)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            ORDER BY [u].[Id]
            OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY

             */


            return Ok(result);
        }

        /// <summary>
        /// Select with concatenation logic
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="surname">Surname</param>
        /// <returns>Result</returns>
        [HttpGet("SelectConcatenate")]
        [ProducesDefaultResponseType(typeof(UserOutput))]
        public async Task<IActionResult> SelectConcatenate([FromQuery] string name, [FromQuery] string surname)
        {
            var temp = db.Users.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                temp = (from u in db.Users where u.Name.Contains(name) select u);

            if (!string.IsNullOrEmpty(surname))
                temp = (from u in temp where u.Surname.Contains(surname) select u);

            var result = await (from u in temp
                                select new UserOutput()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    BirthDate = u.BirthDate
                                }).ToListAsync();
            return Ok(result);

            /*
            QUERY
            
            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            WHERE [u].[Name] LIKE @__name_0_contains ESCAPE N'\' AND [u].[Surname] LIKE @__surname_1_contains ESCAPE N'\'
            
             */
        }


        /// <summary>
        /// Select with IN condition from list
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("SelectIn")]
        [ProducesDefaultResponseType(typeof(UserOutput))]
        public async Task<IActionResult> SelectIn()
        {
            List<int> ids = [1, 2, 3];
            var result = await
                (from u in db.Users
                 where ids.Contains(u.Id)
                 select new UserOutput()
                 {
                     Id = u.Id,
                     Name = u.Name,
                     Surname = u.Surname,
                     BirthDate = u.BirthDate
                 }).ToListAsync();
            return Ok(result);

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            WHERE [u].[Id] IN (
            SELECT [i].[value]
            FROM OPENJSON(@__ids_0) WITH ([value] int '$') AS [i]

            */

        }


        /// <summary>
        /// Select with IN condition from list from query
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("SelectInFromQuery")]
        [ProducesDefaultResponseType(typeof(UserOutput))]
        public async Task<IActionResult> SelectInFromQuery()
        {
            var result = await
                (from u in db.Users
                 where db.Exams.Select(e => e.StudentId).Contains(u.Id)
                 select new UserOutput()
                 {
                     Id = u.Id,
                     Name = u.Name,
                     Surname = u.Surname,
                     BirthDate = u.BirthDate
                 }).ToListAsync();
            return Ok(result);

            /* 
             QUERY
             
             SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
             FROM [User] AS [u]
             WHERE [u].[Id] IN (
             SELECT [e].[StudentId]
             FROM [Exam] AS [e]
             )
             
             */
        }


        /// <summary>
        /// Select with Inheritance (one to one relation)
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>Result</returns>
        [HttpGet("SelectFirstIhnerit/{id}")]
        [ProducesDefaultResponseType(typeof(UserOutput))]
        public async Task<IActionResult> SelectFirstIhnerit([FromRoute] int id)
        {
            var result = await (from u in db.Students
                                where u.Id == id
                                select new UserOutput()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    BirthDate = u.BirthDate
                                }).FirstOrDefaultAsync();

            return Ok(result);

            /*
            QUERY

            SELECT TOP(1) [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            INNER JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
            WHERE [u].[Id] = @__id_0

            */
        }


        /// <summary>
        /// Join
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("Join")]
        [ProducesDefaultResponseType(typeof(List<StudentExamOutput>))]
        public async Task<IActionResult> Join()
        {
            var result = await (
                from u in db.Students
                join e in db.Exams on u.Id equals e.StudentId
                select new StudentExamOutput()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    BirthDate = u.BirthDate,
                    Vote = e.Vote,
                    ExamDate = e.Date,
                    Passed = e.Vote.HasValue
                }).ToListAsync();

            return Ok(result);

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate], [e].[Vote], [e].[Date] AS [ExamDate], CASE
            WHEN [e].[Vote] IS NOT NULL THEN CAST(1 AS bit)
            ELSE CAST(0 AS bit)
            END AS [Passed]
            FROM [User] AS [u]
            INNER JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
            INNER JOIN [Exam] AS [e] ON [u].[Id] = [e].[StudentId]

             */
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("JoinLambda")]
        [ProducesDefaultResponseType(typeof(List<StudentExamOutput>))]
        public async Task<IActionResult> JoinLambda()
        {

            var result = await (db.Students.Join(db.Exams, student => student.Id, exam => exam.StudentId, (s, e) =>
            new StudentExamOutput()
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Surname = s.Surname,
                BirthDate = s.BirthDate,
                ExamDate = e.Date,
                Vote = e.Vote,
                Passed = e.Vote.HasValue
            }).ToListAsync());

            return Ok(result);

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate], [e].[Vote], [e].[Date] AS [ExamDate], CASE
            WHEN [e].[Vote] IS NOT NULL THEN CAST(1 AS bit)
            ELSE CAST(0 AS bit)
            END AS [Passed]
            FROM [User] AS [u]
            INNER JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
            INNER JOIN [Exam] AS [e] ON [u].[Id] = [e].[StudentId]

            */
        }


        /// <summary>
        /// Left Join
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("LeftJoin")]
        [ProducesDefaultResponseType(typeof(List<StudentExamOutput>))]
        public async Task<IActionResult> LeftJoin()
        {
            var result = await (
                from u in db.Students
                join e in db.Exams on u.Id equals e.StudentId into gResult
                from g in gResult.DefaultIfEmpty()
                select new StudentExamOutput()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    BirthDate = u.BirthDate,
                    Vote = g.Vote,
                    ExamDate = g.Date,
                    Passed = g.Vote.HasValue
                }).ToListAsync();

            return Ok(result);

            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate], [e].[Vote], [e].[Date] AS [ExamDate], CASE
            WHEN [e].[Vote] IS NOT NULL THEN CAST(1 AS bit)
            ELSE CAST(0 AS bit)
            END AS [Passed]
            FROM [User] AS [u]
            INNER JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
            LEFT JOIN [Exam] AS [e] ON [u].[Id] = [e].[StudentId]

            */
        }


        /// <summary>
        /// Complex Query
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("ComplexQuery")]
        [ProducesDefaultResponseType(typeof(List<UserExtOutput>))]
        public async Task<IActionResult> Complex()
        {
            var result = await db.Users.Include(u => u.Addresses).Select(u => new UserExtOutput()
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                BirthDate = u.BirthDate,
                Addresses = u.Addresses.Select(a => new AddressOutput()
                {
                    Id = a.Id,
                    City = a.City,
                    Street = a.Street,
                }).ToList()
            }).ToListAsync();

            return Ok(result);


            /*
            QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate], [t].[Id], [t].[City], [t].[Street], [t].[AddressesId], [t].[UsersId]
            FROM [User] AS [u]
            LEFT JOIN (
                SELECT [a0].[Id], [a0].[City], [a0].[Street], [a].[AddressesId], [a].[UsersId]
                FROM [AddressUser] AS [a]
                INNER JOIN [Address] AS [a0] ON [a].[AddressesId] = [a0].[Id]
            ) AS [t] ON [u].[Id] = [t].[UsersId]
            ORDER BY [u].[Id], [t].[AddressesId], [t].[UsersId]

            */
        }

        /// <summary>
        /// Complex result with split query
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("SplitResult")]
        [ProducesDefaultResponseType(typeof(List<UserExtOutput>))]
        public async Task<IActionResult> SplitResult()
        {

            var result = await db.Users.Include(u => u.Addresses).AsSplitQuery().Select(u => new UserExtOutput()
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                BirthDate = u.BirthDate,
                Addresses = u.Addresses.Select(a => new AddressOutput()
                {
                    Id = a.Id,
                    City = a.City,
                    Street = a.Street,
                }).ToList()
            }).ToListAsync();

            return Ok(result);


            /*
            FIRST QUERY

            SELECT [u].[Id], [u].[Name], [u].[Surname], [u].[BirthDate]
            FROM [User] AS [u]
            ORDER BY [u].[Id]

            SECOND QUERY
            
            SELECT [t].[Id], [t].[City], [t].[Street], [u].[Id]
            FROM [User] AS [u]
            INNER JOIN (
                SELECT [a0].[Id], [a0].[City], [a0].[Street], [a].[UsersId]
                FROM [AddressUser] AS [a]
                INNER JOIN [Address] AS [a0] ON [a].[AddressesId] = [a0].[Id]
            ) AS [t] ON [u].[Id] = [t].[UsersId]
            ORDER BY [u].[Id]

            */
        }

        /// <summary>
        /// Simple Group by
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("GroupBy")]
        [ProducesDefaultResponseType(typeof(List<StatsOutput>))]
        public async Task<IActionResult> GroupBy()
        {

            var result = await (from e in db.Exams
                                group e by e.CourseId
                         into g
                                select new StatsOutput
                                {

                                    Id = g.Key,
                                    AverageVote = g.Average(x => x.Vote),
                                    BestVote = g.Max(x => x.Vote),
                                    StudentCount = g.Count()
                                }
                         ).ToListAsync();



            return Ok(result);


            /*
            QUERY

            SELECT [e].[CourseId] AS [Id], AVG(CAST([e].[Vote] AS float)) AS [AverageVote], MAX([e].[Vote]) AS [BestVote], COUNT(*) AS [StudentCount]
            FROM [Exam] AS [e]
            GROUP BY [e].[CourseId]
             
             */
        }


        /// <summary>
        /// Group By with join and having
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("ComplexGroupBy")]
        [ProducesDefaultResponseType(typeof(List<CourseOutput>))]
        public async Task<IActionResult> GroupByComplex()
        {

            var result = await (from c in db.Courses
                                join e in db.Exams on c.Id equals e.CourseId
                                group new { c, e } by new { c.Id, c.Name }
                         into g
                                where g.Average(x => x.e.Vote) > 18

                                select new CourseOutput
                                {
                                    Name = g.Key.Name,
                                    Id = g.Key.Id,
                                    AverageVote = g.Average(x => x.e.Vote),
                                    BestVote = g.Max(x => x.e.Vote),
                                    StudentCount = g.Count()
                                }
                         ).ToListAsync();

            return Ok(result);

            /*
            Query
            
            SELECT[c].[Name], [c].[Id], AVG(CAST([e].[Vote] AS float)) AS[AverageVote], MAX([e].[Vote]) AS[BestVote], COUNT(*) AS[StudentCount]
            FROM[Course] AS[c]
            INNER JOIN[Exam] AS[e] ON[c].[Id] = [e].[CourseId]
            GROUP BY[c].[Id], [c].[Name]
            HAVING AVG(CAST([e].[Vote] AS float)) > 18.0E0
            
             */
        }





        /// <summary>
        /// Union
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("Union")]
        [ProducesDefaultResponseType(typeof(List<KeyValue>))]
        public async Task<IActionResult> Union()
        {
            var result =
                await
                (from u in db.Users
                 select new KeyValue()
                 {
                     Id = u.Id,
                     Value = u.Name,
                 }).Union(
                    from c in db.Courses
                    select new KeyValue()
                    {
                        Id = c.Id,
                        Value = c.Name,
                    }
                    ).ToListAsync();

            return Ok(result);


            /* 
             QUERY
             
             SELECT [u].[Id], [u].[Name] AS [Value]
             FROM [User] AS [u]
             LEFT JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
             UNION
             SELECT [c].[Id], [c].[Name] AS [Value]
             FROM [Course] AS [c]
             
             */
        }


        /// <summary>
        /// Raw
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet("Raw")]
        [ProducesDefaultResponseType(typeof(List<AddressOutput>))]
        public async Task<IActionResult> Raw()
        {
            SqlParameter p = new("@City", "Rome");

            var result = await db.Database
                .SqlQueryRaw<AddressOutput>("SELECT Id,Street,City from Address where City = @City", p)
                .ToListAsync();

            return Ok(result);
        }


        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="input">Course Information</param>
        /// <returns>Result</returns>
        [HttpPost("Insert")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> Insert([FromBody] CourseInput input)
        {

            var result = db.Courses.Add(new Course
            {
                Name = input.Name,
                Description = input.Description
            });

            await db.SaveChangesAsync();
            return Ok(result.Entity.Id);


            /*
            QUERY

            INSERT INTO [Course] ([Description], [Name])
            OUTPUT INSERTED.[Id]
            VALUES (@p0, @p1);
            
             */
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="input">Course Information</param>
        /// <returns>Result</returns>
        [HttpPost("MultiInsert")]
        public async Task<IActionResult> MultiInsert([FromBody] CourseInput input)
        {
            Student? student = await db.Students.FirstOrDefaultAsync();

            if (student == null)
                return NotFound();

            db.Exams.Add(new Exam()
            {
                Student = student,
                Course = new Course()
                {
                    Name = input.Name,
                    Description = input.Description
                },

            });


            await db.SaveChangesAsync();
            return Ok();

            /*
            QUERY

            SELECT TOP(1) [u].[Id], [u].[BirthDate], [u].[Name], [u].[Surname], [s].[Code]
            FROM [User] AS [u]
            INNER JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]

            INSERT INTO [Course] ([Description], [Name])
            OUTPUT INSERTED.[Id]
            VALUES (@p0, @p1);

            INSERT INTO [Exam] ([CourseId], [StudentId], [Date], [Vote])
            VALUES (@p2, @p3, @p4, @p5);
            
             */
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <param name="input">Course Information</param>
        /// <returns>Result</returns>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CourseInput input)
        {
            var course = await (from c in db.Courses where c.Id == id select c).FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            course.Name = input.Name;
            course.Description = input.Description;

            await db.SaveChangesAsync();
            return Ok();

            /*
            COMMAND

            SELECT TOP(1) [c].[Id], [c].[Description], [c].[Name]
            FROM [Course] AS [c]
            WHERE [c].[Id] = @__id_0

            UPDATE [Course] SET [Description] = @p0, [Name] = @p1
            OUTPUT 1
            WHERE [Id] = @p2;

             */
        }


        /// <summary>
        /// Update with Bulk Logic
        /// </summary>
        /// <returns>Result</returns>
        [HttpPut("BulkUpdate")]
        public async Task<IActionResult> BulkUpdate()
        {
            await db.Exams
                 .Where(e => !e.Date.HasValue)
                 .ExecuteUpdateAsync(e => e
                 .SetProperty(e => e.Date, DateTime.Now)
                 .SetProperty(e => e.Vote, 18));


            return Ok();

            /*
            COMMAND

            UPDATE [e]
            SET [e].[Vote] = 18,
                [e].[Date] = GETDATE()
            FROM [Exam] AS [e]
            WHERE [e].[Date] IS NULL

             */
        }

        /// <summary>
        /// Update with join
        /// </summary>
        /// <param name="courseName">course name</param>
        /// <returns>Result</returns>
        [HttpPut("ConditionUpdate/{courseName}")]
        public async Task<IActionResult> ConditionUpdate([FromRoute] string courseName)
        {


            await db.Exams
                .Join(db.Courses, e => e.CourseId, c => c.Id, (e, c) => new { Course = c, Exam = e })
                .Where(g => g.Course.Name.Contains(courseName))
                .ExecuteUpdateAsync(e => e.SetProperty(x => x.Exam.Date, DateTime.Now));
            return Ok();

            /*
            COMMAND

            UPDATE [e]
            SET [e].[Date] = GETDATE()
            FROM [Exam] AS [e]
            INNER JOIN [Course] AS [c] ON [e].[CourseId] = [c].[Id]
            WHERE [c].[Name] LIKE @__courseName_0_contains ESCAPE N'\'

             */
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>Result</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var course = await (from c in db.Courses where c.Id == id select c).FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            db.Courses.Remove(course);

            await db.SaveChangesAsync();
            return Ok();


            /*
            FIRST COMMAND

            SELECT TOP(1) [c].[Id], [c].[Description], [c].[Name]
            FROM [Course] AS [c]
            WHERE [c].[Id] = @__id_0

            SECOND COMMAND

            DELETE FROM [Course]
            OUTPUT 1
            WHERE [Id] = @p0;


            */
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <returns>Result</returns>
        [HttpDelete("BulkDelete")]
        public async Task<IActionResult> DeleteBulk()
        {
            await db.Exams.Where(e => !e.Date.HasValue).ExecuteDeleteAsync();
            return Ok();


            /*
            COMMAND

            DELETE FROM [e]
            FROM [Exam] AS [e]
            WHERE [e].[Date] IS NULL

            */
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <returns>Result</returns>
        [HttpPost("ResetDB")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> ResetDB()
        {

            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            return Ok();
        }

        /// <summary>
        /// Transaction
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="input"></param>
        /// <returns>Result</returns>
        [HttpPost("Transaction/{userId}")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Transaction([FromRoute] int userId, [FromBody] UserInput input)
        {
            using var transaction = db.Database.BeginTransaction();

            //get current student
            var current = (from s in db.Students where s.Id == userId select s).FirstOrDefault();

            if (current == null)
            {
                transaction.Rollback();
                return NotFound("Rollback");
            }

            //store his code
            string currentCode = current.Code;

            //add new student with temporary code to respect unique constraints
            var newStudent = new Student()
            {
                BirthDate = input.BirthDate,
                Name = input.Name,
                Surname = input.Surname,
                Code = "......"
            };

            db.Students.Add(newStudent);

            //save to get id from Database
            db.SaveChanges();

            //update exams replacing student
            db.Exams.Where(e => e.StudentId == userId).ExecuteUpdate(e => e.SetProperty(x => x.StudentId, newStudent.Id));

            //delete current student
            db.Students.Remove(current);


            //replace code
            newStudent.Code = current.Code;

            //save to send update
            db.SaveChanges();

            //commit
            await transaction.CommitAsync();

            /*
            
           
            SELECT TOP(1) [u].[Id], [u].[BirthDate], [u].[Name], [u].[Surname], [s].[Code]
            FROM [User] AS [u]
            INNER JOIN [Student] AS [s] ON [u].[Id] = [s].[Id]
            WHERE [u].[Id] = @__userId_0
            
            INSERT INTO [User] ([BirthDate], [Name], [Surname])
            OUTPUT INSERTED.[Id]
            VALUES (@p0, @p1, @p2);
            
            INSERT INTO [Student] ([Id], [Code])
            VALUES (@p3, @p4);
            
            UPDATE [e]
            SET [e].[StudentId] = @__newStudent_Id_1
            FROM [Exam] AS [e]
            WHERE [e].[StudentId] = @__userId_0
            
            DELETE FROM [Student]
            OUTPUT 1
            WHERE [Id] = @p0;
            
            UPDATE [Student] SET [Code] = @p1
            OUTPUT 1
            WHERE [Id] = @p2;
            
            DELETE FROM [User]
            OUTPUT 1
            WHERE [Id] = @p3;


            */


            return Ok("Commit");
        }

        /// <summary>
        /// Change Tracker
        /// </summary>
        /// <returns></returns>
        [HttpPut("ChangeTracker")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> ChangeTracker()
        {
            StringBuilder sb = new();
            EntityEntry<User>? entry = null;

            
            void print(string title)
            {
                sb.AppendLine("========================");
                sb.AppendLine(title);
                sb.AppendLine($"Tracked Items: {db.ChangeTracker.Entries().Count()}");
                sb.AppendLine();

                if (entry != null)
                {
                    sb.AppendLine();
                    sb.AppendLine($"{entry}");

                    sb.AppendLine();
                    sb.AppendLine("Properties");
                    sb.AppendLine("NAME            ORIGINAL                 CURRENT                  ");


                    entry.Properties.ToList().ForEach(p =>
                    {
                        string name = p.Metadata.Name.PadRight(16, ' ');
                        string original = (p.OriginalValue?.ToString() ?? "").PadRight(25, ' ');
                        string current = (p.CurrentValue?.ToString() ?? "").PadRight(25, ' ');

                        sb.AppendLine($"{name}{original}{current}");

                    });
                }

                sb.AppendLine();
            }

            //no tracking
            print("Initial Status");

            //do a select
            var user = await db.Users.FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            entry = db.Entry(user);
            

            //now EF is tracking our object

            print("After a Select");

            //update object
            user.Name = "Test";

            print("After Edit");

            //set as unchanged
            db.ChangeTracker.Entries<User>().First().State = EntityState.Unchanged;

            print("Set as Unchanged");

            db.ChangeTracker.Entries().First().State = EntityState.Detached;

            user.BirthDate = DateTime.Now;

            print("Set as Detached");

            return Ok(sb.ToString());
        }

    }
}
