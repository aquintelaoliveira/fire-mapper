using FireMapper;
using FireMapper.Test.Domain;
using FireSource;
using System;
using System.Collections.Generic;

namespace FireMapperBench
{
    class Program
    {
        static void Main(string[] args)
        {
            initWeakDataSource(); // init data source
            
            string input = null;
            bool toContinue = true;
            while(toContinue)
            {
                Console.WriteLine();
                Console.WriteLine("########## BENCHMARKING OPTIONS ##########");
                Console.WriteLine("1 - Benchmarking for methond: GetAll");
                Console.WriteLine("2 - Benchmarking for methond: GetById");
                Console.WriteLine("3 - Benchmarking for methond: Add");
                Console.WriteLine("4 - Benchmarking for methond: Update");
                Console.WriteLine("5 - Benchmarking for methond: Add & Delete");
                Console.WriteLine("0 - exit");
                Console.WriteLine();
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        NBench.Bench(BenchDataFireMapperGetAllWithClasses, "BenchDataFireMapperGetAllWithClasses");
                        NBench.Bench(BenchDynamicFireMapperGetAllWithClasses, "BenchDynamicFireMapperGetAllWithClasses");
                        NBench.Bench(BenchDataFireMapperGetAllWithStructs, "BenchDataFireMapperGetAllWithStructs");
                        NBench.Bench(BenchDynamicFireMapperGetAllWithStructs, "BenchDynamicFireMapperGetAllWithStructs");
                        break;
                    case "2":
                        NBench.Bench(BenchDataFireMapperGetByIdWithClasses, "BenchDataFireMapperGetByIdWithClasses");
                        NBench.Bench(BenchDynamicFireMapperGetByIdWithClasses, "BenchDynamicFireMapperGetByIdWithClasses");
                        NBench.Bench(BenchDataFireMapperGetByIdWithStructs, "BenchDataFireMapperGetByIdWithStructs");
                        NBench.Bench(BenchDynamicFireMapperGetByIdWithStructs, "BenchDynamicFireMapperGetByIdWithStructs");
                        break;
                    case "3":
                        NBench.Bench(BenchDataFireMapperAddWithClasses, "BenchDataFireMapperAddWithClasses");
                        NBench.Bench(BenchDynamicFireMapperAddWithClasses, "BenchDynamicFireMapperAddWithClasses");
                        NBench.Bench(BenchDataFireMapperAddWithStructs, "BenchDataFireMapperAddWithStructs");
                        NBench.Bench(BenchDynamicFireMapperAddWithStructs, "BenchDynamicFireMapperAddWithStructs");
                        break;
                    case "4":
                        NBench.Bench(BenchDataFireMapperUpdateWithClasses, "BenchDataFireMapperUpdateWithClasses");
                        NBench.Bench(BenchDynamicFireMapperUpdateWithClasses, "BenchDynamicFireMapperUpdateWithClasses");
                        NBench.Bench(BenchDataFireMapperUpdateWithStructs, "BenchDataFireMapperUpdateWithStructs");
                        NBench.Bench(BenchDynamicFireMapperUpdateWithStructs, "BenchDynamicFireMapperUpdateWithStructs");
                        break;
                    case "5":
                        NBench.Bench(BenchDataFireMapperAddAndDeleteWithClasses, "BenchDataFireMapperAddAndDeleteWithClasses");
                        NBench.Bench(BenchDynamicFireMapperAddAndDeleteWithClasses, "BenchDynamicFireMapperAddAndDeleteWithClasses");
                        NBench.Bench(BenchDataFireMapperAddAndDeleteWithStructs, "BenchDataFireMapperAddAndDeleteWithStructs");
                        NBench.Bench(BenchDynamicFireMapperAddAndDeleteWithStructs, "BenchDynamicFireMapperAddAndDeleteWithStructs");
                        break;
                    case "0":
                        toContinue = false;
                        break;
                    default:
                        Console.WriteLine($"Unknown option {input}");
                        break;
                }
            }
        }

        // Fire Mapper with classes
        static readonly FireDataMapper fc = new FireDataMapper(typeof(Course), new WeakDataSourceFactory());
        static readonly DynamicFireMapper dc = new DynamicFireMapper(typeof(Course), new WeakDataSourceFactory());
        // // Fire Mapper with structs
        static readonly FireDataMapper fs = new FireDataMapper(typeof(CourseStruct), new WeakDataSourceFactory());
        static readonly DynamicFireMapper ds = new DynamicFireMapper(typeof(CourseStruct), new WeakDataSourceFactory());

        #region DataFireMapper - class
        static void BenchDataFireMapperGetAllWithClasses()
        {
            fc.GetAll();
        }
        static void BenchDataFireMapperGetByIdWithClasses()
        {
            fc.GetById("bench-course-1-id");
        }
        static void BenchDataFireMapperAddWithClasses()
        {
            fc.Add(new Course(
                "bench-course-0-id",
                "Course 0",
                new CourseType(
                    "bench-course-type-0-id",
                    "Type 0",
                    new ProgrammingLanguage(
                        "bench-programming-language-0-id",
                        "Language 0"
                        )
                    ),
                "Course 0 Description",
                2.22,
                new Instructor(
                    "bench-instructor-0-id",
                    "Instructor 0",
                    "instructor0@email.com"
                    )
                )
            );
        }
        static void BenchDataFireMapperUpdateWithClasses()
        {
            fc.Update(new Course(
                "bench-course-1-id",
                "Updated Course 1",
                new CourseType(
                    "bench-course-type-1-id",
                    "Type 1",
                    new ProgrammingLanguage(
                        "bench-programming-language-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new Instructor(
                    "bench-instructor-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
        }
        static void BenchDataFireMapperAddAndDeleteWithClasses()
        {
            fc.Add(new Course(
                "bench-course-to-delete-1-id",
                "Course 1",
                new CourseType(
                    "bench-course-type-to-delete-1-id",
                    "Type 1",
                    new ProgrammingLanguage(
                        "bench-programming-language-to-delete-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new Instructor(
                    "bench-instructor-to-delete-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
            fc.Delete("bench-course-to-delete-1-id");
        }
        #endregion

        #region DynamicFireMapper - class
        static void BenchDynamicFireMapperGetAllWithClasses()
        {
            dc.GetAll();
        }
        static void BenchDynamicFireMapperGetByIdWithClasses()
        {
            dc.GetById("bench-course-1-id");
        }
        static void BenchDynamicFireMapperAddWithClasses()
        {
            dc.Add(new Course(
                "bench-course-0-id",
                "Course 0",
                new CourseType(
                    "bench-course-type-0-id",
                    "Type 0",
                    new ProgrammingLanguage(
                        "bench-programming-language-0-id",
                        "Language 0"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new Instructor(
                    "bench-instructor-0-id",
                    "Instructor 0",
                    "instructor0@email.com"
                    )
                )
            );
        }
        static void BenchDynamicFireMapperUpdateWithClasses()
        {
            dc.Update(new Course(
                "bench-course-1-id",
                "Updated Course 1",
                new CourseType(
                    "bench-course-type-1-id",
                    "Type 1",
                    new ProgrammingLanguage(
                        "bench-programming-language-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new Instructor(
                    "bench-instructor-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
        }
        static void BenchDynamicFireMapperAddAndDeleteWithClasses()
        {
            dc.Add(new Course(
                "bench-course-to-delete-1-id",
                "Course 1",
                new CourseType(
                    "bench-course-type-to-delete-1-id",
                    "Type 1",
                    new ProgrammingLanguage(
                        "bench-programming-language-to-delete-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new Instructor(
                    "bench-instructor-to-delete-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
            dc.Delete("bench-course-to-delete-1-id");
        }
        #endregion

        #region DataFireMapper - structs
        static void BenchDataFireMapperGetAllWithStructs()
        {
            fs.GetAll();
        }
        static void BenchDataFireMapperGetByIdWithStructs()
        {
            fs.GetById("bench-course-1-id");
        }
        static void BenchDataFireMapperAddWithStructs()
        {
            fs.Add(new CourseStruct(
                "bench-course-0-id",
                "Course 0",
                new CourseTypeStruct(
                    "bench-course-type-0-id",
                    "Type 0",
                    new ProgrammingLanguageStruct(
                        "bench-programming-language-0-id",
                        "Language 0"
                        )
                    ),
                "Course 0 Description",
                2.22,
                new InstructorStruct(
                    "bench-instructor-0-id",
                    "Instructor 0",
                    "instructor0@email.com"
                    )
                )
            );
        }
        static void BenchDataFireMapperUpdateWithStructs()
        {
            fs.Update(new CourseStruct(
                "bench-course-1-id",
                "Updated Course 1",
                new CourseTypeStruct(
                    "bench-course-type-1-id",
                    "Type 1",
                    new ProgrammingLanguageStruct(
                        "bench-programming-language-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new InstructorStruct(
                    "bench-instructor-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
        }
        static void BenchDataFireMapperAddAndDeleteWithStructs()
        {
            fs.Add(new CourseStruct(
                "bench-course-to-delete-1-id",
                "Course 1",
                new CourseTypeStruct(
                    "bench-course-type-to-delete-1-id",
                    "Type 1",
                    new ProgrammingLanguageStruct(
                        "bench-programming-language-to-delete-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new InstructorStruct(
                    "bench-instructor-to-delete-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
            fs.Delete("bench-course-to-delete-1-id");
        }
        #endregion

        #region DynamicFireMapper - structs
        static void BenchDynamicFireMapperGetAllWithStructs()
        {
            ds.GetAll();
        }
        static void BenchDynamicFireMapperGetByIdWithStructs()
        {
            ds.GetById("bench-course-1-id");
        }
        static void BenchDynamicFireMapperAddWithStructs()
        {
            ds.Add(new CourseStruct(
                "bench-course-0-id",
                "Course 0",
                new CourseTypeStruct(
                    "bench-course-type-0-id",
                    "Type 0",
                    new ProgrammingLanguageStruct(
                        "bench-programming-language-0-id",
                        "Language 0"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new InstructorStruct(
                    "bench-instructor-0-id",
                    "Instructor 0",
                    "instructor0@email.com"
                    )
                )
            );
        }
        static void BenchDynamicFireMapperUpdateWithStructs()
        {
            ds.Update(new CourseStruct(
                "bench-course-1-id",
                "Updated Course 1",
                new CourseTypeStruct(
                    "bench-course-type-1-id",
                    "Type 1",
                    new ProgrammingLanguageStruct(
                        "bench-programming-language-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new InstructorStruct(
                    "bench-instructor-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
        }
        static void BenchDynamicFireMapperAddAndDeleteWithStructs()
        {
            ds.Add(new CourseStruct(
                "bench-course-to-delete-1-id",
                "Course 1",
                new CourseTypeStruct(
                    "bench-course-type-to-delete-1-id",
                    "Type 1",
                    new ProgrammingLanguageStruct(
                        "bench-programming-language-to-delete-1-id",
                        "Language 1"
                        )
                    ),
                "Course 1 Description",
                2.22,
                new InstructorStruct(
                    "bench-instructor-to-delete-1-id",
                    "Instructor 1",
                    "instructor1@email.com"
                    )
                )
            );
            ds.Delete("bench-course-to-delete-1-id");
        }
        #endregion

        private static void initWeakDataSource()
        {
            WeakDataSource courseWDS = new WeakDataSource("Courses", "Id");
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
            for (var i = 1; i < 11; i++)
            {
                InsertCourseFor(courseWDS, $"bench-course-{i}-id", $"Course {i}", $"bench-course-type-{i}-id", $"Course {i} Description", 1.11, $"bench-instructor-{i}-id");
            }

            WeakDataSource courseTypeWDS = new WeakDataSource("Course Types", "Id");
            void InsertCourseTypesFor(IDataSource dataSource, string id, string type, string programmingLanguageId)
            {
                dataSource.Add(new Dictionary<string, object>() {
                    {"Id", id},
                    {"Type", type},
                    {"ProgrammingLanguage", programmingLanguageId},
                });
            }
            for (var i = 1; i < 11; i++)
            {
                InsertCourseTypesFor(courseTypeWDS, $"bench-course-type-{i}-id", $"Type {i}", $"bench-programming-language-{i}-id");
            }

            WeakDataSource programmingLanguagesWDS = new WeakDataSource("Programming Languages", "Id");
            void InsertProgrammingLanguagesFor(IDataSource dataSource, string id, string language)
            {
                dataSource.Add(new Dictionary<string, object>() {
                    {"Id", id},
                    {"Language", language},
                });
            }
            for (var i = 1; i < 11; i++)
            {
                InsertProgrammingLanguagesFor(programmingLanguagesWDS, $"bench-programming-language-{i}-id", $"Language {i}");
            }

            WeakDataSource instructorWDS = new WeakDataSource("Instructors", "Id");
            void InsertInstructorFor(IDataSource dataSource, string id, string name, string email)
            {
                dataSource.Add(new Dictionary<string, object>() {
                    {"Id", id},
                    {"Name", name},
                    {"Email", email},
                });
            }
            for (var i = 1; i < 11; i++)
            {
                InsertInstructorFor(instructorWDS, $"bench-instructor-{i}-id", $"Instructor {i}", $"instructor{i}@email.com");
            }
        }
    }
}

