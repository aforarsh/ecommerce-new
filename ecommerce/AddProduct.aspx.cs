﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecommerce
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
/*            if (!IsPostBack)
            {
                if (Session["admin@admin.com"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }*/
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-20VP0PUP\SQLEXPRESS;Initial Catalog=ecommerce;Integrated Security=True;");
            if (imageUpload.HasFile)
            {
                string filename = imageUpload.PostedFile.FileName;
                string filepath = "Images/" + imageUpload.FileName;
                imageUpload.PostedFile.SaveAs(Server.MapPath("~/Images/") + filename);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tb_product(Product_Name,Product_Desc,Product_Img,Product_Price,Product_qty,Product_Category) VALUES('" + txtName.Text + "', '" + txtDesc.Text + "', '" + filepath + "', '" + txtPrice.Text + "', '" + txtQuantity.Text + "', '" + DropDownList1.SelectedItem.Text + "' )", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Product Added Successfully');</script>");
                Response.Redirect("AddProduct.aspx");
            }
        }
    }
}