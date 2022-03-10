using System;
using System.Collections.Generic;

using FireSource;

namespace FireMapper.Test
{
    /// <summary>
    /// A single test context shared among all the tests.
    /// Test classes should implement IClassFixture<FireMapperFixture> and
    /// provide a constructor to inject a Fixture object.
    /// </summary>
    public class FireMapperFixture : IDisposable
    {
        #region WeakDataSource
        public readonly WeakDataSourceFactory WeakDataSourceFactory = new WeakDataSourceFactory();
        public WeakDataSource instructorWDS;
        public WeakDataSource courseWDS;
        public WeakDataSource courseTypeWDS;
        public WeakDataSource programmingLanguagesWDS;
        #endregion

        #region FireDataSource
        public readonly FireDataSourceFactory FireDataSourceFactory = new FireDataSourceFactory();
        public FireDataSource instructorFDS;
        public FireDataSource courseFDS;
        public FireDataSource courseTypeFDS;
        public FireDataSource programmingLanguagesFDS;
        #endregion

        public void InstantiateDataSource()
        {
            #region WeakDataSource
            CallWeakDataSourceFactory(WeakDataSourceFactory, out instructorWDS, "Instructors", "Id");
            CallWeakDataSourceFactory(WeakDataSourceFactory, out courseWDS, "Courses", "Id");
            CallWeakDataSourceFactory(WeakDataSourceFactory, out courseTypeWDS, "Course Types", "Id");
            CallWeakDataSourceFactory(WeakDataSourceFactory, out programmingLanguagesWDS, "Programming Languages", "Id");
            #endregion
            #region FireDataSource
            CallFireDataSourceFactory(FireDataSourceFactory, out instructorFDS, "Instructors", "Id");
            CallFireDataSourceFactory(FireDataSourceFactory, out courseFDS, "Courses", "Id");
            CallFireDataSourceFactory(FireDataSourceFactory, out courseTypeFDS, "Course Types", "Id");
            CallFireDataSourceFactory(FireDataSourceFactory, out programmingLanguagesFDS, "Programming Languages", "Id");
            #endregion
        }
        public static void CallWeakDataSourceFactory(WeakDataSourceFactory factory, out WeakDataSource dataSource, string collection, string key)
        {
            dataSource = (WeakDataSource)factory.CreateDataSource(collection, key);
        }
        public static void CallFireDataSourceFactory(FireDataSourceFactory factory, out FireDataSource dataSource, string collection, string key)
        {
            dataSource = (FireDataSource)factory.CreateDataSource(collection, key);
        }

        public void Dispose()
        {
            ///
            /// ... clean up test data from the database ...
            /// 
            Clear(instructorFDS, "Id");
            Clear(courseFDS, "Id");
            Clear(courseTypeFDS, "Id");
            Clear(programmingLanguagesFDS, "Id");
        }
        private static void Clear(IDataSource source, string key) {
            IEnumerable<Dictionary<string, object>> docs = source.GetAll();
            foreach(var pairs in docs) 
            {
                source.Delete(pairs[key]);
            }
        }

        public FireMapperFixture()
        {
            InstantiateDataSource();
            Dispose();
            CreateInstructors();
            CreateCourses();
            CreateCourseTypes();
            CreateProgrammingLanguages();
        }

        void CreateInstructors()
        {
            IDataSource[] DataSources = { instructorWDS, instructorFDS };
            foreach (IDataSource ds in DataSources)
            {
                InsertInstructorFor(ds, "fixture-instructor-1-id", "Instructor 1", "instructor1@email.com");
                InsertInstructorFor(ds, "fixture-instructor-2-id", "Instructor 2", "instructor2@email.com");
                InsertInstructorFor(ds, "fixture-instructor-3-id", "Instructor 3", "instructor3@email.com");
            }
        }
        void InsertInstructorFor(IDataSource dataSource, string id, string name, string email)
        {
            dataSource.Add(new Dictionary<string, object>() {
                {"Id", id},
                {"Name", name},
                {"Email", email},
            });
        }

        void CreateCourses()
        {
            IDataSource[] DataSources = { courseWDS, courseFDS };
            foreach (IDataSource ds in DataSources)
            {
                InsertCourseFor(ds, "fixture-course-1-id", "Course 1", "fixture-course-type-1-id", "Course 1 Description", 1.11, "fixture-instructor-1-id");
                InsertCourseFor(ds, "fixture-course-2-id", "Course 2", "fixture-course-type-2-id", "Course 2 Description", 2.22, "fixture-instructor-1-id");
                InsertCourseFor(ds, "fixture-course-3-id", "Course 3", "fixture-course-type-3-id", "Course 3 Description", 3.33, "fixture-instructor-3-id");
            }
        }
        void InsertCourseFor(IDataSource dataSource, string id, string name, 
            string courseTypeId, string description, double price, string instructorId)
        {
            dataSource.Add(new Dictionary<string, object>() {
                {"Id", id},
                {"Name", name},
                {"CourseType", courseTypeId},
                {"Description", description},
                {"Price", price},
                {"Instructor", instructorId}
            });
        }

        void CreateCourseTypes()
        {
            IDataSource[] DataSources = { courseTypeWDS, courseTypeFDS };
            foreach (IDataSource ds in DataSources)
            {
                InsertCourseTypesFor(ds, "fixture-course-type-1-id", "Type 1", "fixture-programming-language-1-id");
                InsertCourseTypesFor(ds, "fixture-course-type-2-id", "Type 2", "fixture-programming-language-2-id");
                InsertCourseTypesFor(ds, "fixture-course-type-3-id", "Type 3", "fixture-programming-language-3-id");
            }
        }
        void InsertCourseTypesFor(IDataSource dataSource, string id, string type, string programmingLanguageId)
        {
            dataSource.Add(new Dictionary<string, object>() {
                {"Id", id},
                {"Type", type},
                {"ProgrammingLanguage", programmingLanguageId},
            });
        }

        void CreateProgrammingLanguages()
        {
            IDataSource[] DataSources = { programmingLanguagesWDS, programmingLanguagesFDS };
            foreach (IDataSource ds in DataSources)
            {
                InsertProgrammingLanguagesFor(ds, "fixture-programming-language-1-id", "Language 1");
                InsertProgrammingLanguagesFor(ds, "fixture-programming-language-2-id", "Language 2");
                InsertProgrammingLanguagesFor(ds, "fixture-programming-language-3-id", "Language 3");
            }
        }
        void InsertProgrammingLanguagesFor(IDataSource dataSource, string id, string language)
        {
            dataSource.Add(new Dictionary<string, object>() {
                {"Id", id},
                {"Language", language},
            });
        }
    }
}