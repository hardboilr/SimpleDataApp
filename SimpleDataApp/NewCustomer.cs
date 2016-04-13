using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleDataApp {
    public partial class NewCustomer : Form {

        private int parsedCustomerID;
        private int orderID;
        private string connstr = SimpleDataApp.Utility.GetConnectionString();

        public NewCustomer() {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e) {
            if (isCustomerName()) {

                SqlConnection conn = new SqlConnection(connstr);

                //NC-7 Create a SqlCommand, and identify it as a stored procedure.
                SqlCommand cmdNewCustomer = new SqlCommand("Sales.uspNewCustomer", conn);
                cmdNewCustomer.CommandType = CommandType.StoredProcedure;

                //NC-8 Add input parameter from the stored procedure and specify what to use as its value.
                cmdNewCustomer.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar, 40));
                cmdNewCustomer.Parameters["@CustomerName"].Value = txtCustomerName.Text;

                //NC-9 Add output parameter.
                cmdNewCustomer.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                cmdNewCustomer.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

                try {
                    conn.Open();

                    //NC-12 Run the stored procedure.
                    cmdNewCustomer.ExecuteNonQuery();

                    //NC-13 Customer ID is an IDENTITY value from the database. 
                    this.parsedCustomerID = (int)cmdNewCustomer.Parameters["@CustomerID"].Value;
                    this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
                } catch {
                    //NC-14 A simple catch.
                    MessageBox.Show("Customer ID was not returned. Account could not be created.");
                } finally {
                    //NC-15 Close the connection.
                    conn.Close();
                }
            }
        }

        private bool isCustomerName() {
            if (txtCustomerName.Text == "") {
                MessageBox.Show("Please enter a name.");
                return false;
            } else {
                return true;
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e) {
            if (isPlaceOrderReady()) {

                SqlConnection conn = new SqlConnection(connstr);

                //NC-19 Create SqlCommand and identify it as a stored procedure.
                SqlCommand cmdNewOrder = new SqlCommand("Sales.uspPlaceNewOrder", conn);
                cmdNewOrder.CommandType = CommandType.StoredProcedure;

                //NC-20 @CustomerID, which was an output parameter from uspNewCustomer.
                cmdNewOrder.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                cmdNewOrder.Parameters["@CustomerID"].Value = this.parsedCustomerID;

                //NC-21 @OrderDate.
                cmdNewOrder.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime, 8));
                cmdNewOrder.Parameters["@OrderDate"].Value = dtpOrderDate.Value;

                //NC-22 @Amount.
                cmdNewOrder.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                cmdNewOrder.Parameters["@Amount"].Value = numOrderAmount.Value;

                //NC-23 @Status. For a new order, the status is always O (open)
                cmdNewOrder.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1));
                cmdNewOrder.Parameters["@Status"].Value = "O";

                //NC-24 Add return value for stored procedure, which is the orderID.
                cmdNewOrder.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int));
                cmdNewOrder.Parameters["@RC"].Direction = ParameterDirection.ReturnValue;

                try {
                    conn.Open();

                    cmdNewOrder.ExecuteNonQuery();

                    this.orderID = (int)cmdNewOrder.Parameters["@RC"].Value;
                    MessageBox.Show("Order number " + this.orderID + " has been submitted.");
                } catch {
                    MessageBox.Show("Order could not be placed.");
                } finally {
                    conn.Close();
                }



            }
        }

        private bool isPlaceOrderReady() {
            if (txtCustomerID.Text == "") {
                MessageBox.Show("Please create customer account before placing order.");
                return false;
            } else if ((numOrderAmount.Value < 1)) {
                MessageBox.Show("Please specify an order amount");
                return false;
            } else {
                return true;
            }
        }

        private void btnAddAnotherAccount_Click(object sender, EventArgs e) {
            this.ClearForm();
        }

        private void ClearForm() {
            txtCustomerName.Clear();
            txtCustomerID.Clear();
            dtpOrderDate.Value = DateTime.Now;
            numOrderAmount.Value = 0;
            this.parsedCustomerID = 0;
        }

        private void btnAddFinish_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
