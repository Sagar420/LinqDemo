using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    /// <summary>
    ///  Linq Demo
    ///  Types of Linq
    ///  1-Objet Collection-Linq to Object
    ///  2-ADO.net Dataset- Linq to Dataset
    ///  3-XML Document- Linq to XML 
    ///  4-Entity Framework- Linq to Entities
    ///  5- Sql Database- Linq to Sql
    ///  6- Other Data Source- By implementing Iqueryable
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Types of Query Operator
            IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
            new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
            new Student() { StudentID = 2, StudentName = "Bill",  Age = 45 } ,
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };

            IList<Standard> standardList = new List<Standard>() {
            new Standard(){ StandardID = 1, StandardName="Standard 1"},
            new Standard(){ StandardID = 2, StandardName="Standard 2"},
            new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };

            //------------------------------------------Where Operator---------------------------------------------------------------//

            // Use LINQ to find teenager students
            Student[] teenAgeStudent = studentList.Where(s => s.Age > 12 && s.Age < 20).ToArray();

            // Use LINQ to find first student whose name is Bill 
            Student bill = studentList.Where(s => s.StudentName == "Bill").FirstOrDefault();

            // Use LINQ to find student whose StudentID is 5
            Student studId = studentList.Where(s => s.StudentID == 5).FirstOrDefault();

            var studentID = from s in studentList where s.StudentID.Equals(5) select s;


            //------------------------------------------OfType Operator=Get Type of selected data---------------------------------------------------------------//
            ArrayList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

            var stringResult = mixedList.OfType<string>();

            var intResult = mixedList.OfType<int>();


            var intResult1 = from s in mixedList.OfType<string>() select s;

            //------------------------------------------OrderBy Operator---------------------------------------------------------------//

            var orderByResult = from s in studentList orderby s.StudentName select s;

            var descByResult = from s in studentList orderby s.StudentName descending select s;

            var orderby = studentList.OrderBy(s => s.StudentName);

            //------------------------------------------ThenBy Operator= Sort data using multiple field---------------------------------------------------------------//

            var thenByResult = studentList.OrderBy(s => s.StudentName).ThenBy(s => s.Age);

            //------------------------------------------GroupBy Operator---------------------------------------------------------------//

            var groupResult = from s in studentList group s by s.Age;

            var groupLam = studentList.GroupBy(s => s.Age);

            //------------------------------------------ToLookup Operator---------------------------------------------------------------//

            var lookupResult = studentList.ToLookup(s => s.Age);

            //------------------------------------------Join Operator---------------------------------------------------------------//

            var joinResult = from s in studentList
                             join a in standardList
                             on s.StudentID equals a.StandardID
                             select new
                             {
                                 s.StudentID,
                                 s.StudentName,
                                 a.StandardName
                             };

            var joinLam = studentList.Join(standardList,
                                        s => s.StudentID,
                                        a => a.StandardID,
                                        (s, a) => new
                                        {
                                            s.StudentID,
                                            s.StudentName,
                                            a.StandardName
                                        });

            //------------------------------------------GroupJoin Operator---------------------------------------------------------------//
            //Value ChildValue Normal join Result
            //  A       a1
            //  A       a2
            //  A       a3
            //  B       b1
            //  B       b2

            //Value ChildValues GroupJoin Result
            //  A       [a1, a2, a3]
            //  B       [b1, b2]
            //  C       []

            var groupjoinResult = from s in standardList
                                  join a in studentList
                             on s.StandardID equals a.StudentID
                             into studentgroup
                             select new
                             {
                                 Students = studentgroup,
                                 StandardName = s.StandardName
                             };

            var groupjoinLam = standardList.GroupJoin(studentList,
                                        s => s.StandardID,
                                        a => a.StudentID,
                                        (s, a) => new
                                        {
                                            Students = a,
                                            StandardName = s.StandardName
                                        });
            
            //------------------------------------------Select Operator---------------------------------------------------------------//

            var selectResult = from s in studentList
                               select s.StudentName;

            var selectNewResult = from s in studentList
                                  select new { s.StudentID, s.StudentName };

            var selectLam = studentList.Select(s => s.StudentID);

            var selectNewLam = studentList.Select(s => new { s.StudentID,s.StudentName});

            //------------------------------------------All Any Operator---------------------------------------------------------------//

            // checks whether all the students are teenagers 
            bool AllResult = studentList.All(s => s.Age > 12 && s.Age < 20);

            bool AnyResult = studentList.Any(s => s.Age > 12 && s.Age < 20);

            //------------------------------------------Contains Operator---------------------------------------------------------------//

            var containResult = studentList.Where(s => s.StudentName.Contains("S"));

            //------------------------------------------Aggregate Operator---------------------------------------------------------------//

            IList<String> strList = new List<String>() { "One", "Two", "Three", "Four", "Five", "Six" };
            var aggregateResult = strList.Aggregate((s1, s2) => s1 + ", " + s2);

            //------------------------------------------Average Operator---------------------------------------------------------------//

            var avgResult = studentList.Average(s => s.Age);

            //------------------------------------------Count Operator---------------------------------------------------------------//

            var countResult = strList.Count();

            //------------------------------------------Max Operator---------------------------------------------------------------//

            var maxResult = studentList.Max(s=>s.Age);

            //------------------------------------------Sum Operator---------------------------------------------------------------//

            var sumResult = studentList.Sum(s => s.Age);

            //------------------------------------------ElementAt and ElementAtOrDefault Operator---------------------------------------------------------------//

            var elementResult = strList.ElementAt(4);

            var elementDefaultResult = strList.ElementAtOrDefault(5);

            //------------------------------------------First and FirstOrDefault Operator---------------------------------------------------------------//

            var firstResult = strList.First();

            var firstOrDefaultResult = strList.FirstOrDefault();

            //------------------------------------------Last and LastOrDefault Operator---------------------------------------------------------------//

            var lastResult = strList.Last();

            var lastOrDefaultResult = strList.LastOrDefault();

            //------------------------------------------Single and SingleOrDefault Operator---------------------------------------------------------------//

            //var singleResult = strList.Single();

            //var singleOrDefaultResult = strList.SingleOrDefault();

            //------------------------------------------SequenceEqual Operator=check no of element and sequence---------------------------------------------------------------//

            IList<string> strList1 = new List<string>() { "One", "Two", "Three", "Four", "Three" };

            IList<string> strList2 = new List<string>() { "One", "Two", "Three", "Four", "Three" };

            bool sequenceResult = strList1.SequenceEqual(strList2);

            //------------------------------------------Concat Operator---------------------------------------------------------------//

            var concatResult = strList1.Concat(strList2);

            //------------------------------------------DefaultIfEmpty Operator---------------------------------------------------------------//

            IList<Student> emptyStudentList = new List<Student>();

            var defaultResult = emptyStudentList.DefaultIfEmpty();

            //------------------------------------------Empty, Range, Repeat Operator---------------------------------------------------------------//

            var emptyCollection1 = Enumerable.Empty<string>();
            var emptyCollection2 = Enumerable.Empty<Student>();

            Console.WriteLine("Count: {0} ", emptyCollection1.Count());
            Console.WriteLine("Type: {0} ", emptyCollection1.GetType().Name);

            Console.WriteLine("Count: {0} ", emptyCollection2.Count());
            Console.WriteLine("Type: {0} ", emptyCollection2.GetType().Name);


            Console.WriteLine("\n Range result:-");
            var rangeResult = Enumerable.Range(10, 10);

            for (int i = 0; i < rangeResult.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, rangeResult.ElementAt(i));

            Console.WriteLine("\n Repeat result:-");
            var intCollection = Enumerable.Repeat<int>(25, 10);

            for (int i = 0; i < intCollection.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

            //------------------------------------------Distinct Operator---------------------------------------------------------------//

            IList<string> strListTest = new List<string>() { "One", "Two", "Three", "Two", "Three" };

            IList<int> intListTest = new List<int>() { 1, 2, 3, 2, 4, 4, 3, 5 };

            var distinctLam = strListTest.Distinct();

            var distinctResult = from s in strListTest select s.Distinct();


            //------------------------------------------Except Operator---------------------------------------------------------------//

            IList<string> strList11 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            IList<string> strList22 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight" };

            var exceptResult = strList11.Except(strList22);

            //------------------------------------------Distinct Operator---------------------------------------------------------------//

            IList<Student> studentList1 = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };

            IList<Student> studentList2 = new List<Student>() {
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };

            var intersectResult = studentList1.Intersect(studentList2);

            //------------------------------------------Union Operator---------------------------------------------------------------//

            IList<string> strList111 = new List<string>() { "One", "Two", "three", "Four" };
            IList<string> strList222 = new List<string>() { "Two", "THREE", "Four", "Five" };

            var unionResult = strList111.Union(strList222);

            //------------------------------------------Skip,SkipWhile Operator---------------------------------------------------------------//


            var skipResult = strList.Skip(2);

            var resultList = strList.SkipWhile(s => s.Length < 4);

            foreach (string str in resultList)
                Console.WriteLine(str);

            //------------------------------------------Take,TakeWhile Operator---------------------------------------------------------------//

            var takeResult = strList.Take(2);

            var takeResultData = strList.TakeWhile(s => s.Length > 4);

            foreach (string str in takeResultData)
                Console.WriteLine(str);

            //------------------------------------------Conversion Operator---------------------------------------------------------------//

            Student[] studentArray = {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 } ,
            };


            var enumerableResult = studentArray.AsEnumerable();

            var querableResult = studentArray.AsQueryable();

            var castResult = studentArray.Cast<Student>();

            IList<string> strListResult = new List<string>() {
                                            "One",
                                            "Two",
                                            "Three",
                                            "Four",
                                            "Three"
                                            };

            var strArray = strListResult.ToArray();

            var strLista = strListResult.ToList();

            IDictionary<int, Student> studentDict =
                                studentList.ToDictionary<Student, int>(s => s.StudentID);
            

            Console.WriteLine();
        }
    }

    public class Student {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }

    public class Standard
    {
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }
}
