using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;


// 1. Отправить схемы таблиц в БД
// 2. Отправить данные из методов FILL в бд
// 3. Вручную добавить в таблицы БД по 1 значению
// 4. Скачать изменения в локальный DataSet
// 5. Использовать фабрику провайдеров
// FlightsDB
namespace AutoLevelLesson
{
    class Program
    {
        private static DataTable peopleTable;
        private static DataTable userTable;

        static void Main(string[] args)
        {
            string connectionString = "Server = A - 305 - 13; Database = shop; Trusted_Connection = True;";
            var dataset = new DataSet("shop");
            var userTable = new DataTable("users");
            userTable.Columns.Add(new DataColumn
            {
                ColumnName = "id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int)

            });
            userTable.PrimaryKey = new DataColumn[] { userTable.Columns["id"] };
            userTable.Columns.Add(new DataColumn
            {
                ColumnName = "personId",
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int)

            });

            var peopleTable = new DataTable("people");
            peopleTable.Columns.Add(new DataColumn
            {
                ColumnName = "id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int)

            });
            peopleTable.Columns.Add(new DataColumn
            {
                ColumnName = "fullName",
                AllowDBNull = false,
                Unique = false,
                DataType = typeof(string)

            });
            dataset.Tables.AddRange(new DataTable[]
            {
                        userTable, peopleTable
            });
            //dataset.Relations.Add(peopleTable.Columns["id"], userTable.Columns["peopleId"]);
            foreach(DataTable dt in dataset.Tables)
            {
                Console.WriteLine(dt.TableName);
            }
            //FillPeople();
            //FillUsers();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            var connection = new SqlConnection(connectionString);
            var selectCommand = new SqlCommand("insert * from Table", connection);

            dataAdapter.SelectCommand = selectCommand;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            //dataAdapter.Fill(dataset); // получает данные из БД
            //dataset.AcceptChanges();
            //dataAdapter.Update(dataset);//обновляет бд исходя из локального dataset


        }

        private static void FillUsers()
        {
            userTable.Rows.Add(1, 1);
        }

        private static void FillPeople()
        {
            var idRow = peopleTable.NewRow();
            idRow.ItemArray = new object[] { 1, "Petrovich" };
            peopleTable.Rows.Add(idRow);

            peopleTable.Rows.Add(2, "Vasilich");
        }

        private static void FillPeoples()
        {
            DataRow peopleRow = peopleTable.NewRow();
            peopleRow["fdsfsdsf"] = "BMW";
            userTable.Rows.Add(carRow);

            carRow = inventoryTable.NewRow();
            carRow[1] = "Saab";
            carRow[2] = "Red";
            carRow[3] = "Sea Breeze";
            inventoryTable.Rows.Add(carRow);
        }
    }
}

