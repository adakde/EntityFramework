using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using MyBoards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Benchmark
{
    [MemoryDiagnoser]
    public class TrackingBenchmark
    {
        [Benchmark]
        public int WithTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyboardsContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyBoardsDb;Trusted_Connection=True;");
            var _dbContext = new MyboardsContext(optionsBuilder.Options);

            var comments = _dbContext.comments.ToList();
            return comments.Count;
        }
        [Benchmark]
        public int WithNoTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyboardsContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyBoardsDb;Trusted_Connection=True;");
            var _dbContext = new MyboardsContext(optionsBuilder.Options);

            var comments = _dbContext.comments
                .AsNoTracking()
                .ToList();
            return comments.Count;
        }
    }
}
