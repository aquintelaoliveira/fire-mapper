using System;
using System.Collections.Generic;
using System.IO;

namespace FireSource.Test
{
    /// <summary>
    /// A single test context shared among all the tests.
    /// Test classes should implement IClassFixture<WeakStoreFixture> and
    /// provide a constructor to inject a Fixture object.
    /// </summary>
    public class WeakStoreFixture : IDisposable
    {

        const string SOURCE_ITEMS = "Resources/isel-AVE-2021.txt";

        public readonly WeakDataSource studentsDb = new WeakDataSource(
                "Students", // Collection
                "Number"    // key field
            );
        public readonly WeakDataSource classroomsDb = new WeakDataSource(
                "Classrooms",   // Collection
                "Token"         // key field
            );

        public void Dispose()
        {
            ///
            /// ... clean up test data from the database ...
            /// 
            Clear(studentsDb, "Number");
            Clear(classroomsDb, "Token");
        }
        private static void Clear(WeakDataSource source, string key) {
            IEnumerable<Dictionary<string, object>> docs = source.GetAll();
            foreach(var pairs in docs) 
            {
                source.Delete(pairs[key]);
            }
        }

        public WeakStoreFixture()
        {
            CreateClassrooms();
            AddToWeakStoreFrom(SOURCE_ITEMS);
        }
        void CreateClassrooms()
        {
            InsertClassroomFor("TLI41D", "Miguel Gamboa");
            InsertClassroomFor("TLI42D", "Luís Falcão");
            InsertClassroomFor("TLI41N", "Miguel Gamboa");
            InsertClassroomFor("TLI4NXST", "NA");
            InsertClassroomFor("TLI4DXST", "NA");
        }
        void InsertClassroomFor(string token, string teacher) {
            classroomsDb.Add(new Dictionary<string, object>() {
                {"Teacher", teacher},
                {"Token", token},
            });
        }

        void AddToWeakStoreFrom(string path)
        {
            foreach (string line in Lines(path))
            {
                Student st = Student.Parse(line);
                studentsDb.Add(new Dictionary<string, object>() {
                    {"Name", st.Name}, 
                    {"Number", st.Number},
                    {"Classroom", st.Classroom}, 
                });
            }
        }

        static IEnumerable<string> Lines(string path)
        {
            string line;
            IList<string> res = new List<string>();
            using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
            {
                while ((line = file.ReadLine()) != null)
                {
                    res.Add(line);
                }
            }
            return res;
        }
    }
}