using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace homework8
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionInfo = new Connection
            {
                Server = "JASURBEKABDUVOH\\SQLEXPRESS",
                Database = "homeworkdb",
                Login = "sa",
                Password = "Root123."
            };

            var connection = new SqlConnection(connectionInfo.ToString());

            try
            {
                connection.Open();
                if (connection.State != ConnectionState.Open)
                {
                    fmt.Println("connection failed", ConsoleColor.Red);
                    return;
                }
            }
            catch (Exception exception)
            {
                fmt.Println(exception.ToString(), ConsoleColor.Red);
                throw;
            }

            fmt.Println("┌──────────────────────────────────┐", ConsoleColor.Green);
            fmt.Println("│  choose action to proceed:       │", ConsoleColor.Green);
            fmt.Println("│    1 => add new person           │", ConsoleColor.Green);
            fmt.Println("│    2 => delete existing person   │", ConsoleColor.Green);
            fmt.Println("│    3 => select all               │", ConsoleColor.Green);
            fmt.Println("│    4 => select by id             │", ConsoleColor.Green);
            fmt.Println("│    5 => update existing person   │", ConsoleColor.Green);
            fmt.Println("└──────────────────────────────────┘", ConsoleColor.Green);

            var command = connection.CreateCommand();
            var action = fmt.Scan();
            switch (action)
            {
                case "1":
                {
                    var person = new Person();
                    fmt.Print("FirstName: ");
                    person.FirstName = fmt.Scan();
                    fmt.Print("LastName: ");
                    person.LastName = fmt.Scan();
                    fmt.Print("MiddleName: ");
                    person.MiddleName = fmt.Scan();
                    fmt.Print("BirthDate: ");
                    person.BirthDate = Convert.ToDateTime(fmt.Scan());

                    if (Insert(command, person) > 0)
                        fmt.Println("person added", ConsoleColor.Green);
                    else
                        fmt.Println("unable to add person", ConsoleColor.Red);
                    break;
                }
                case "2":
                {
                    fmt.Print("Id: ");
                    var id = Convert.ToInt32(fmt.Scan());

                    if (Delete(command, id) > 0)
                        fmt.Println("person deleted", ConsoleColor.Green);
                    else
                        fmt.Println("unable to delete person", ConsoleColor.Red);
                    break;
                }
                case "3":
                {
                    var persons = SelectAll(command);
                    for (var i = 0; i < persons.Count; i++)
                    {
                        fmt.Println(persons[i].ToString(), i % 2 == 0 ? ConsoleColor.Green : ConsoleColor.Yellow);
                    }

                    break;
                }
                case "4":
                {
                    fmt.Print("Id: ");
                    var id = Convert.ToInt32(fmt.Scan());

                    var person = SelectById(command, id);
                    fmt.Println(person.ToString(), ConsoleColor.Green);

                    break;
                }
                case "5":
                {
                    var person = new Person();
                    fmt.Print("Id: ");
                    person.Id = Convert.ToInt32(fmt.Scan());
                    fmt.Print("FirstName: ");
                    person.FirstName = fmt.Scan();
                    fmt.Print("LastName: ");
                    person.LastName = fmt.Scan();
                    fmt.Print("MiddleName: ");
                    person.MiddleName = fmt.Scan();
                    fmt.Print("BirthDate: ");
                    person.BirthDate = Convert.ToDateTime(fmt.Scan());
                    if (Update(command, person) > 0)
                        fmt.Println("person updated", ConsoleColor.Green);
                    else
                        fmt.Println("unable to update person", ConsoleColor.Red);
                    break;
                }
                default:
                    fmt.Println("action not found, exiting");
                    break;
            }
        }

        private static int Insert(IDbCommand command, Person person)
        {
            command.CommandText = "insert into Person(" +
                                  "FirstName," +
                                  "LastName," +
                                  "MiddleName," +
                                  "BirthDate) values (" +
                                  $"'{person.FirstName}'," +
                                  $"'{person.LastName}'," +
                                  $"'{person.MiddleName}'," +
                                  $"'{person.BirthDate}');";

            return command.ExecuteNonQuery();
        }

        private static int Update(IDbCommand command, Person person)
        {
            command.CommandText = "update Person set " +
                                  $"FirstName = '{person.FirstName}'," +
                                  $"LastName = '{person.LastName}'," +
                                  $"MiddleName = '{person.MiddleName}'," +
                                  $"BirthDate = '{person.BirthDate}'" +
                                  $"where Id = {person.Id};";

            return command.ExecuteNonQuery();
        }

        private static int Delete(IDbCommand command, int id)
        {
            command.CommandText = "delete from Person where " +
                                  $"Id = {id};";

            return command.ExecuteNonQuery();
        }

        private static List<Person> SelectAll(SqlCommand command)
        {
            command.CommandText = "select * from Person;";

            var reader = command.ExecuteReader();

            var people = new List<Person>();
            while (reader.Read())
            {
                var person = new Person
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    MiddleName = reader["MiddleName"].ToString(),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"])
                };
                people.Add(person);
            }

            reader.Close();

            return people;
        }

        private static Person SelectById(SqlCommand command, int id)
        {
            command.CommandText = $"select * from Person where Id = {id};";

            var reader = command.ExecuteReader();
            reader.Read();
            var person = new Person
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                MiddleName = reader["MiddleName"].ToString(),
                BirthDate = Convert.ToDateTime(reader["BirthDate"])
            };

            reader.Close();

            return person;
        }
    }
}