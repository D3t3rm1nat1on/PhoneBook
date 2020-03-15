using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace PhoneBook
{
    class Program
    {
        private static int _accessMode;
        private static bool _exit = false;

        private const int TableWidth = 150;
        private static NpgsqlConnection _connection;
        private static string _connString;
        private static ConsoleColor queryColor = ConsoleColor.Yellow;
        private static ConsoleColor tableColor = ConsoleColor.DarkGray;
        private static ConsoleColor columnNameColor = ConsoleColor.DarkBlue;
        private static ConsoleColor cellColor = ConsoleColor.Blue;

        private static string[][] _enambeCommands =
        {
            new[] {"show", "find", "exit"},
            new[] {"show", "exit", "add", "find", "remove"}
        };

        private static Dictionary<string, Action> _actions = new Dictionary<string, Action>()
        {
            {
                "add", () =>
                {
                    Console.Write("name: ");
                    string name = Console.ReadLine();
                    Console.Write("phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("mobile: ");
                    string mobile = Console.ReadLine();
                    Console.Write("position: ");
                    string position = Console.ReadLine();
                    Console.Write("department: ");
                    string department = Console.ReadLine();

                    if (name.Length > 120 ||
                        phone.Length != 5 ||
                        !int.TryParse(phone, out _) ||
                        mobile.Length > 20 ||
                        position.Length > 50 ||
                        department.Length > 50)
                    {
                        Console.WriteLine("incorrect data");
                        return;
                    }

                    string sqlQuery =
                        "insert into phonebook (name, phone, mobile, position, department) values " +
                        $"('{name}','{phone}','{mobile}','{position}','{department}')";
                    var command = new NpgsqlCommand(sqlQuery, _connection);
                    command.ExecuteNonQuery();

                }
            },
            {
                "remove", () =>
                {
                    Console.Write("Введите id записи: ");
                    if (int.TryParse(Console.ReadLine(), out int temp))
                    {
                        string sqlQuery = $"delete from phonebook where id = {temp}";
                        var command = new NpgsqlCommand(sqlQuery, _connection);
                        command.ExecuteNonQuery();
                        Console.WriteLine("complete");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода");
                    }
                }
            },
            {
                "show", () =>
                {
                    string sqlQuery = "select * from phonebook";
                    PrintResultQuery(sqlQuery);
                    
                }

            },
            {
                "find", () =>
                {
                    Console.WriteLine("Выберите по каким параметрам Вы желаете найти пользователя:");
                    Console.WriteLine("Name, Position, Department");
                    string choise = Console.ReadLine();
                    string sqlQuery = "";
                    switch (choise)
                    {
                        case "Name":
                        {
                            Console.WriteLine("Введите Имя");
                            choise = Console.ReadLine();
                            sqlQuery = "select * from phonebook" +
                                       " where \"Name\" = '" + choise + "';";
                        }
                            break;
                        case "Position":
                        {
                            Console.WriteLine("Введите Должность");
                            choise = Console.ReadLine();
                            sqlQuery = "select * from phonebook" +
                                       " where \"Position\" = '" + choise + "';";
                        }
                            break;
                        case "Department":
                        {
                            Console.WriteLine("Введите Департамент");
                            choise = Console.ReadLine();
                            sqlQuery = "select * from phonebook" +
                                       " where \"Department\" = '" + choise + "';";
                        }
                            break;
                        
                    }
                    if(sqlQuery!="") PrintResultQuery(sqlQuery);

                }
            }, 
            {
                "exit", () =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Завершение работы");
                    _exit = true;
                }
            }
        };

        private static void Main()
        {

            // ------------------------- ПОДКЛЮЧЕНИЕ К БД ------------------------- //

            try
            {
                _connString = "Server=localhost;Port=5432;User Id=postgres;Password=77031985;Database=PhoneBook_DB";

              //  _connString = "Server=localhost;Port=5432;User Id=postgres;Password=12345;Database=PhoneBook_DB";
                _connection = new NpgsqlConnection(_connString);
                _connection.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Удачное подключение");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не удалось подключиться");
                return;
            }

            Console.ForegroundColor = default;

            // ------------------ ИНИЦИАЛИЗАЦИЯ ПОЛЬЗОВАТЕЛИЯ --------------------- //

            Console.WriteLine("Введите режим пользователя (admin, pathetic hooman)");
            string command;
            while (true)
            {
                command = Console.ReadLine();

                if (command == "pathetic hooman")
                {
                    _accessMode = 0;
                    break;
                }

                if (command == "admin")
                {
                    _accessMode = 1;
                    break;
                }
            }

            // ----------------------------- ОСНОВНОЙ ЦИКЛ ------------------------ //

            while (!_exit)
            {
                Console.WriteLine("Возможные комманды: " + string.Join(", ", _enambeCommands[_accessMode]));
                command = Console.ReadLine();
                while (!_enambeCommands[_accessMode].Contains(command))
                {
                    command = Console.ReadLine();
                }

                _actions[command].Invoke();
            }



        }

        #region PrintFunctions

        static void PrintQuery(string query)
        {
            Console.ForegroundColor = queryColor;
            Console.WriteLine(query);
            Console.ForegroundColor = default;
        }

        static void PrintResultQuery(string query)
        {
            PrintQuery(query);

            var command = new NpgsqlCommand(query, _connection);
            var reader = command.ExecuteReader();

            int count = reader.FieldCount;

            if (!reader.HasRows)
            {
                Console.WriteLine("Совпадения не найдены");
                reader.Close();
                return; 
            }
               
            string[] columnNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                columnNames[i] = reader.GetName(i);
            }

            PrintLine(count);
            PrintRow(columnNameColor, columnNames);
            PrintLine(count);

            while (reader.Read())
            {
                string[] values = new string [count];

                for (int i = 0; i < count; i++)
                {
                    values[i] = reader.GetValue(i).ToString();
                }

                PrintRow(cellColor, values);
                PrintLine(count);
            }

            reader.Close();
        }

        static void PrintLine(int countOfRows)
        {
            int width = (TableWidth - countOfRows) / countOfRows;
            string line = "+";
            ;
            for (int i = 0; i < countOfRows; i++)
            {
                line += new string('-', width) + '+';
            }

            Console.ForegroundColor = tableColor;
            Console.WriteLine(line);
            Console.ForegroundColor = default;
        }

        static void PrintRow(ConsoleColor color, params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row;

            Console.ForegroundColor = tableColor;
            Console.Write("|");
            foreach (string column in columns)
            {
                row = AlignCentre(column, width);
                Console.ForegroundColor = color;
                Console.Write(row);
                Console.ForegroundColor = tableColor;
                Console.Write("|");
            }

            Console.WriteLine();
            Console.ForegroundColor = default;
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        #endregion
    }

}