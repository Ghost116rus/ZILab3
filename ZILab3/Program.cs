using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class Program
    {
        static int SubjectsCount = 8;
        static int ObjectsCount = 3;

        static int[] matrixsbj = new int[SubjectsCount];
        static int[] matrixobj = new int[ObjectsCount];
        static int[] oldmatrixsbj = new int[SubjectsCount];

        static int HasAccess(string operation, int obj, int sbj)
        {

            if (operation == "read" && (matrixobj[obj] <= matrixsbj[sbj])) return 1;
            if (operation == "write" && (matrixobj[obj] >= matrixsbj[sbj])) return 1;
            if (operation == "change" && (obj <= matrixsbj[sbj])) return 1;
            return 0;
        }
        static void Main(string[] args)
        {
            string[] users = new string[SubjectsCount];
            string[] items = new string[ObjectsCount];
            string[] level = new string[4];
            string userName, obj;
            string operation;
            int user = 0, authControl = 0;

            users[0] = "Admin";
            users[1] = "User1";
            users[2] = "User2";
            users[3] = "User3";
            users[4] = "User4";
            users[5] = "Guest1";
            users[6] = "Guest2";
            users[7] = "Guest3";

            level[0] = "NONCONFIDENTIAL";
            level[1] = "CONFIDENTIAL";
            level[2] = "SECRET";
            level[3] = "TOP_SECRET";

            items[0] = "File1";
            items[1] = "File2";
            items[2] = "File3";

            Random rnd = new Random();
            matrixsbj[0] = 3;
            oldmatrixsbj[0] = 3;
            for (int i = 1; i < SubjectsCount; i++)
            {
                if (!users[i].Contains("Guest"))              
                    matrixsbj[i] = rnd.Next(0, 4);
                else
                    matrixsbj[i] = 0;
                oldmatrixsbj[i] = matrixsbj[i];
            }

            for (int i = 0; i < (ObjectsCount - 1); i++)            
                matrixobj[i] = rnd.Next(0, 4);
            

            while (true)
            {
                while (true)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("ВЫ НАХОДИТЕСЬ В СИСТЕМЕ АВТОРИЗАЦИИ \n");
                    Console.WriteLine("Для прекращения работы введите exit\n");
                    Console.WriteLine("Уровни доступа:\nNONCONFIDENTIAL-0\nCONFIDENTIAL-1\nSECRET-2\nTOP_SECRET-3\n");
                    Console.WriteLine("Объекты по уровню доступа: ");

                    for (int i = 0; i < ObjectsCount; i++)                    
                        Console.WriteLine(items[i] + " - " + matrixobj[i]);                    

                    Console.WriteLine("\nСубъекты по уровню доступа: ");

                    for (int i = 0; i < SubjectsCount; i++)                    
                        Console.WriteLine(users[i] + " - " + matrixsbj[i]);
                    

                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("Введите ваше имя:");
                    userName = Console.ReadLine();
                    if (userName == "exit") return;
                    for (int i = 0; i < SubjectsCount; i++)
                    {
                        if (users[i] == userName)
                        {
                            authControl = 1;
                            user = i;
                            Console.WriteLine("Идентификация прошла успешно. Добро пожаловать, " + userName + ".");
                            break;
                        }
                        else if (i == SubjectsCount)
                        {
                            Console.WriteLine("Такого пользователя нет. \n");
                        }
                    }
                    if (authControl == 1) break;
                }
                authControl = 0;
                while (true)
                {
                    try
                    {
                        Console.Write("\nВведите команду (read, write, change, exit): \n");
                        operation = Console.ReadLine();
                        if (operation == "exit")
                        {
                            Console.WriteLine("Осуществлён выход из системы. Всего доброго, " + userName + ". \n\n");
                            break;
                        }
                        else if (operation == "change")
                        {
                            int operationCode;
                            Console.WriteLine("Операция изменения доступа субьекта\n");
                            while (true)
                            {
                                Console.WriteLine("Уровни доступа:\nNONCONFIDENTIAL-0\nCONFIDENTIAL - 1\nSECRET - 2\nTOP_SECRET - 3\nНачальный уровень - 777\n");
                                Console.WriteLine("Какой уровень доступа вы хотите получить?");
                                operationCode = Convert.ToInt32(Console.ReadLine());
                                if ((operationCode > 3 || operationCode < 0) && operationCode != 777)
                                {
                                    Console.WriteLine("Некорректный ввод. \n");
                                    break;
                                }
                                if (operationCode == 777)
                                {
                                    matrixsbj[user] = oldmatrixsbj[user];
                                    Console.WriteLine(users[user] + " is " + level[matrixsbj[user]]);
                                }
                                else if (HasAccess(operation, operationCode, user) == 1)
                                {
                                    matrixsbj[user] = operationCode;
                                    Console.WriteLine(users[user] + " is " + level[operationCode]);
                                }
                                else
                                {
                                    Console.WriteLine("У вас нет таких прав!");
                                    break;
                                }
                                break;
                            }
                        }
                        else if (operation == "read" || operation == "write")
                        {
                            int operationCode;
                            Console.WriteLine("\nПеречень объектов: ");

                            for (int i = 0; i < ObjectsCount; i++)
                            {
                                Console.WriteLine("[" + i + "] - [" + items[i] + "]");
                            }

                            Console.WriteLine("\nНад каким обьектом выполняется операция? Введите его номер: ");
                            obj = Console.ReadLine();
                            operationCode = Convert.ToInt32(obj);
                            if (HasAccess(operation, operationCode, user) == 1) Console.WriteLine("Процедура прошла успешно. \n");
                            else Console.WriteLine("У вас нет таких прав. \n");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод.\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Некорректный ввод.\n");
                    }
                }
            }
        }
    }

}
