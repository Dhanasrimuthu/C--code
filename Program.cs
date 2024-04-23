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

    public void Display()
    {
        Console.WriteLine("This is the data what you Enter");
        Console.WriteLine($"ID: {Category}");
        Console.WriteLine($"Start_date: {StartDate}");
        Console.WriteLine($"End_date: {EndDate}");
        Console.WriteLine($"Amount: {Amount}");
        Console.WriteLine($"Description: {Description}");
    }
}

class Program
{
    public static void choose()
    {
        Console.WriteLine("Choose an option::");
        Console.WriteLine("Option 1: Insert row into the table");
        Console.WriteLine("Option 2: Delete row from the table");
        Console.WriteLine("Option 3: Edit row value with new value");
        Console.WriteLine("Option 4: show   Data from the table");
        Console.WriteLine("Option 5: Exit from this...");
        int option = int.Parse(Console.ReadLine());
        opt(option);
    }
    public static void Main(String[] args)
    {
        choose();
        Console.WriteLine("press any key to close the console.....");
        Console.ReadKey();
    }
    public static void opt(int option)
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=tourism;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            switch (option)
            {
                case 1:
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
                    Console.WriteLine("Enter the start_date:like(mm-dd)");
                    string Start_date = Console.ReadLine();
                    Console.WriteLine("Enter the End_date: like(mm-dd)");
                    String End_date = Console.ReadLine();
                    Console.WriteLine("Enter the Amount:");
                    decimal Amount = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the Description");
                    String Description = Console.ReadLine();

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
                        Console.WriteLine("Record inserted successfully.");
                    }
                    choose();
                    break;
                      case 2:
                    string showQuery2 = "select * from Expenses_table";
                    using (SqlCommand command = new SqlCommand(showQuery2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Expenses_id  " + "Category  " + "Start_date    " + "End_date       " + "Amount   " + "Description   ");
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                int category = reader.GetInt32(1);
                                string start_date = reader.GetString(2);
                                string end_date = reader.GetString(3);
                                decimal amount = reader.GetDecimal(4);
                                string description = reader.GetString(5);
                                Console.WriteLine(id + "            " + category + "         " + start_date + "        " + end_date + "      " + amount + "       " + description);

                            }
                            reader.Close();
                        }
                    }
                    string deleteQuery = "DELETE FROM Expenses_table Where Expenses_Id=@id";
                    Console.WriteLine("Enter the Expenses_id you want to delete..");
                    int delete_id = int.Parse(Console.ReadLine());
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", delete_id);
                        command.ExecuteNonQuery();
                        Console.WriteLine("Deleted");
                    }
                    choose();
                    break;
                case 3:
                    string showQuery1 = "select * from Expenses_table";
                    using (SqlCommand command = new SqlCommand(showQuery1, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Expenses_id  " + "Category  " + "Start_date    " + "End_date     " + "Amount   " + "Description   ");
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                int category = reader.GetInt32(1);
                                string start_date = reader.GetString(2);
                                string end_date = reader.GetString(3);
                                decimal amount = reader.GetDecimal(4);
                                string description = reader.GetString(5);
                                Console.WriteLine(id + "            " + category + "         " + start_date + "        " + end_date + "      " + amount + "      " + description);

                            }
                            reader.Close();
                        }
                    }
                    Console.WriteLine("Choose an option::");
                    Console.WriteLine("Option 1: Change Amount in the table");
                    Console.WriteLine("Option 2: Change Description in the table");
                    int Value = int.Parse(Console.ReadLine());
                    if (Value == 1)
                    {
                        string updateQuery = "UPDATE Expenses_table SET Amount = @Amount WHERE Expenses_Id = @ExpenseId";
                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            Console.WriteLine("Enter the Expenses_Id you want to change");
                            int id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the Amount you want to change");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            command.Parameters.AddWithValue("@Amount", amount);
                            command.Parameters.AddWithValue("@ExpenseId", id);
                            command.ExecuteNonQuery();
                            Console.WriteLine("In record amount is updated successfully.");
                        }
                    }
                    else if (Value == 2)
                    {
                        string updateQuery = "UPDATE Expenses_table SET Description = @Description WHERE Expenses_Id = @ExpenseId";
                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            Console.WriteLine("Enter the Expenses_Id you want to change");
                            int id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the description you want to change");
                            string amount = Console.ReadLine();
                            command.Parameters.AddWithValue("@Description", amount);
                            command.Parameters.AddWithValue("@ExpenseId", id);
                            command.ExecuteNonQuery();
                            Console.WriteLine("In record description is updated successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid");
                    }
                    choose();
                    break;
                case 4:
                    Console.WriteLine("Choose an option::");
                    Console.WriteLine("Option 1: To show only one record");
                    Console.WriteLine("Option 2: To show all the record");
                    int showValue = int.Parse(Console.ReadLine());
                    if(showValue == 1)
                    {
                        string show = "select * from Expenses_table where Expenses_Id =@ExpenseId";
                        using (SqlCommand command = new SqlCommand(show, connection))
                        {
                            Console.WriteLine("Enter the Expenses_Id you want to view");
                            int id = int.Parse(Console.ReadLine());
                            command.Parameters.AddWithValue("@ExpenseId", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Expenses_id: {reader["Expenses_id"]} Category: {reader["Category"]} Start_date: {reader["Start_date"]} End_date: {reader["End_date"]} Amount: {reader["Amount"]} Description: {reader["Description"]}");
                                }
                            }

                        }
                    }
                    else if (showValue == 2)
                    {
                        string showQuery = "select * from Expenses_table";
                        using (SqlCommand command = new SqlCommand(showQuery, connection))
                        {
                            List<Expense> expenses = new List<Expense>();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Expense expense = new Expense
                                    {
                                        Expenses_id = (int)reader["Expenses_id"],
                                        Category = (int)reader["Category"],
                                        StartDate = reader["Start_date"].ToString(),
                                        EndDate = reader["End_date"].ToString(),
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
                                      Expenses = g.OrderBy(e => e.Expenses_id)
                                  });

                            foreach (var group in groupedExpenses)
                            {
                                Console.WriteLine($"Category: {group.Category}");
                                foreach (var exp in group.Expenses)
                                {
                                    Console.WriteLine($"  Expense_ID: {exp.Expenses_id} Start_Date: {exp.StartDate} End_Date: {exp.EndDate} Amount: {exp.Amount} Description: {exp.Description}");

                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid");
                    }
                    choose();
                    break;
                case 5:
                    Console.WriteLine("Exit..");
                    break;
                default:
                    Console.WriteLine("choose a valid option....");
                    choose();
                    break;
            }
        }
    }
}

