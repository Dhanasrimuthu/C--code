using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

class Expense
{
    public int Expenses_id { get; set; }
    public int Category { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }

    public void Display()
    {
        Console.WriteLine("Expense Details:");
        Console.WriteLine($"ID: {Category}");
        Console.WriteLine($"Start Date: {StartDate}");
        Console.WriteLine($"End Date: {EndDate}");
        Console.WriteLine($"Amount: {Amount}");
        Console.WriteLine($"Description: {Description}");
    }
}

class Program
{
    private static string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=tourism;Integrated Security=True";

    public static void Main(String[] args)
    {
        choose();
        Console.WriteLine("Press any key to close the console.....");
        Console.ReadKey();
    }

    public static void choose()
    {
        Console.WriteLine("===============================");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1: Insert a row into the table");
        Console.WriteLine("2: Delete a row from the table");
        Console.WriteLine("3: Edit a row value");
        Console.WriteLine("4: Show data from the table");
        Console.WriteLine("5: Exit");

        int option = int.Parse(Console.ReadLine());
        opt(option);
    }

    public static void opt(int option)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            switch (option)
            {
                case 1:
                    InsertRecord(connection);
                    choose();
                    break;
                case 2:
                    DeleteRecord(connection);
                    choose();
                    break;
                case 3:
                    EditRecord(connection);
                    choose();
                    break;
                case 4:
                    ShowRecord(connection);
                    choose();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    choose();
                    break;
            }
        }
    }

    private static void InsertRecord(SqlConnection connection)
    {
        try
        {
            string Query = "SELECT * FROM Categories";
            using (SqlCommand Command = new SqlCommand(Query, connection))
            {
                using (SqlDataReader reader = Command.ExecuteReader())
                {
                    Console.WriteLine("categories_id" + "    Place");
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string categories = reader.GetString(1);
                        Console.WriteLine("       " + id + "        " + categories);
                    }
                    reader.Close();
                }
            }
            Console.WriteLine("choose anyone from above");
            int Category = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the start_date:(yyyy-mm-dd)");
            string Start_date = (Console.ReadLine());
            // DateTime Start_date = DateTime.ParseExact(Console.ReadLine());
            Console.WriteLine("Enter the End_date:(yyyy-mm-dd)");
            string End_date = (Console.ReadLine());
            Console.WriteLine("Enter the Amount:");
            decimal Amount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Description");
            string Description = Console.ReadLine();

            Expense pr1 = new Expense();
            pr1.Category = Category;
            pr1.StartDate = Start_date;
            pr1.EndDate = End_date;
            pr1.Amount = Amount;
            pr1.Description = Description;
            Console.WriteLine("==================================");
            pr1.Display();

            string insertQuery = "INSERT INTO Expenses_table (Category, Start_Date, End_Date, Amount, Description) " +
                          "VALUES (@Category, @StartDate, @EndDate, @Amount, @Description)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Category", pr1.Category);
                command.Parameters.AddWithValue("@StartDate", pr1.StartDate);
                command.Parameters.AddWithValue("@EndDate", pr1.EndDate);
                command.Parameters.AddWithValue("@Amount", pr1.Amount);
                command.Parameters.AddWithValue("@Description", pr1.Description);

                command.ExecuteNonQuery();
                Console.WriteLine("------------");
                Console.WriteLine("Record inserted successfully.");
            }
            Console.WriteLine("=======================");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while inserting the record:");
            Console.WriteLine(e.Message);
        }
    }

    private static void DeleteRecord(SqlConnection connection)
    {
        try
        {
            string showQuery2 = "select * from Expenses_table";
            using (SqlCommand command = new SqlCommand(showQuery2, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("ExpensesId  " + "Category  " + "Amount   " + "Description   ");
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int category = reader.GetInt32(1);
                        // DateTime startDate = reader.GetDateTime(2);
                        //DateTime endDate = reader.GetDateTime(3);
                        decimal amount = reader.GetDecimal(4);
                        string description = reader.GetString(5);
                        Console.WriteLine(id + "            " + category + "      " + amount + "       " + description);

                    }
                    reader.Close();
                }
            }
            string deleteQuery = "DELETE FROM Expenses_table Where ExpenseId=@id";
            Console.WriteLine("Enter the ExpenseId you want to delete..");
            int delete_id = int.Parse(Console.ReadLine());
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@id", delete_id);
                int res = command.ExecuteNonQuery();
                Console.WriteLine("------------");
                if (res > 0)
                {
                    Console.WriteLine("A Record is Deleted");
                }
                else
                {
                    Console.WriteLine("Record is not deleted invalid ExpenseId");
                    Console.WriteLine("===========================================");
                    opt(2);
                }
            }
            Console.WriteLine("=================");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the record:");
            Console.WriteLine(e.Message);
        }
    }

    private static void EditRecord(SqlConnection connection)
    {
        try
        {
            string showQuery1 = "select * from Expenses_table";
            using (SqlCommand command = new SqlCommand(showQuery1, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("ExpensesId  " + "Category  " + "Amount   " + "Description   ");
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int category = reader.GetInt32(1);
                        // DateTime startDate = reader.GetDateTime(2);
                        // DateTime endDate = reader.GetDateTime(3);
                        decimal amount = reader.GetDecimal(4);
                        string description = reader.GetString(5);
                        Console.WriteLine(id + "            " + category + "      " + amount + "      " + description);

                    }
                    reader.Close();
                }
            }
            Console.WriteLine("====================");
            Console.WriteLine("Choose an option::");
            Console.WriteLine("Option 1: Change Amount in the table");
            Console.WriteLine("Option 2: Change Description in the table");
            Console.WriteLine("Option 3: Change both Amount & Description");
            int Value = int.Parse(Console.ReadLine());
            if (Value == 1)
            {
                string updateQuery = "UPDATE Expenses_table SET Amount = @Amount WHERE ExpenseId = @ExpenseId";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    Console.WriteLine("Enter the ExpenseId you want to change");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the Amount you want to change");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@ExpenseId", id);
                    int res = command.ExecuteNonQuery();
                    Console.WriteLine("------------");
                    if (res > 0)
                    {
                        Console.WriteLine("In record amount is updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("In record amount is not updated invalid ExpenseId.");
                        Console.WriteLine("===========================================");
                        opt(3);
                    }
                }
            }
            else if (Value == 2)
            {
                string updateQuery = "UPDATE Expenses_table SET Description = @Description WHERE ExpenseId = @ExpenseId";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    Console.WriteLine("Enter the ExpenseId you want to change");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the description you want to change");
                    string description = Console.ReadLine();
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@ExpenseId", id);
                    int res = command.ExecuteNonQuery();
                    Console.WriteLine("------------");
                    if (res > 0)
                    {
                        Console.WriteLine("In record amount is updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("In record amount is not updated invalid ExpenseId.");
                        Console.WriteLine("===========================================");
                        opt(3);
                    }
                }
            }
            else if (Value == 3)
            {
                string updateQuery = "UPDATE Expenses_table SET Amount = @Amount, Description = @Description WHERE ExpenseId = @ExpenseId";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    Console.WriteLine("Enter the ExpenseId you want to change");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the description you want to change");
                    string description = Console.ReadLine();
                    Console.WriteLine("Enter the Amount you want to change");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@ExpenseId", id);
                    int res = command.ExecuteNonQuery();
                    Console.WriteLine("--------");
                    if (res > 0)
                    {
                        Console.WriteLine("In record amount is updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("In record amount is not updated invalid ExpenseId.");
                        Console.WriteLine("===========================================");
                        opt(3);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid");
            }
            Console.WriteLine("=================================");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while updating the record:");
            Console.WriteLine(e.Message);
        }
    }

    private static void ShowRecord(SqlConnection connection)
    {
        Console.WriteLine("----------");
        Console.WriteLine("Choose an option::");
        Console.WriteLine("Option 1: To show only one record");
        Console.WriteLine("Option 2: To show all the record");
        int showValue = int.Parse(Console.ReadLine());
        try
        {
            if (showValue == 1)
            {
                string showid = "select ExpenseId from Expenses_table ";
                using (SqlCommand command = new SqlCommand(showid, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ExpenseId");
                        while (reader.Read())
                        {
                            Console.Write(reader["ExpenseId"] + "    ");
                        }
                        Console.WriteLine();
                    }
                }
                string show = "select * from Expenses_table where ExpenseId =@ExpenseId";
                using (SqlCommand command = new SqlCommand(show, connection))
                {
                    Console.WriteLine("Enter the ExpenseId you want to view");
                    int id = int.Parse(Console.ReadLine());
                    command.Parameters.AddWithValue("@ExpenseId", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ExpensesId: {reader["ExpenseId"]} Category: {reader["Category"]} Start_date: {reader["Start_date"]} End_date: {reader["End_date"]} Amount: {reader["Amount"]} Description: {reader["Description"]}");
                        }
                    }

                }
            }
            else if (showValue == 2)
            {
                //string showQuery = "select * from Expenses_table";
                string showQuery = "SELECT e.ExpenseId, e.Category, c.Name, e.Start_date, e.End_date, e.Amount, e.Description " +
                   "FROM Expenses_table e " +
                   "INNER JOIN Categories c ON e.Category = c.Id";
                using (SqlCommand command = new SqlCommand(showQuery, connection))
                {
                    List<Expense> expenses = new List<Expense>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Expense expense = new Expense
                            {
                                Expenses_id = (int)reader["ExpenseId"],
                                Category = (int)reader["Category"],
                                CategoryName = reader["Name"].ToString(),
                                StartDate = (reader["Start_date"].ToString()),
                                EndDate = (reader["End_date"].ToString()),
                                Amount = (decimal)reader["Amount"],
                                Description = reader["Description"].ToString()
                            };
                            expenses.Add(expense);
                        }
                        reader.Close();
                    }
                    var groupedExpenses = expenses.OrderBy(e => e.Category).GroupBy(e => e.Category)
                          .Select(g => new
                          {
                              Category = g.Key,
                              CategoryName = g.First().CategoryName,
                              Expenses = g.OrderBy(e => e.Expenses_id)
                          });

                    foreach (var group in groupedExpenses)
                    {
                        Console.WriteLine($"Category: {group.CategoryName}");
                        foreach (var exp in group.Expenses)
                        {
                            Console.WriteLine($"  ExpenseId: {exp.Expenses_id} Start_Date: {exp.StartDate} End_Date: {exp.EndDate} Amount: {exp.Amount} Description: {exp.Description}");

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid");
            }
            Console.WriteLine("========================================");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while showing the record:");
            Console.WriteLine(e.Message);
        }
    }
}
