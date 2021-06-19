﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecommerce
{
    public partial class AddCategory : System.Web.UI.Page
    {
        string str = @"Data Source=LAPTOP-20VP0PUP\SQLEXPRESS;Initial Catalog=ecommerce;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowGrid();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(str);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tb_category WHERE Category_Name='" + TextBox1.Text.ToString() + "' ", con1);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count==1)
            {
                Response.Write("<script>alert('This Category is Already Exist');</script>");
            }
            else
            {
                SqlConnection con = new SqlConnection(str);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tb_category VALUES (@Category_Name)", con);
                cmd.Parameters.AddWithValue("@Category_Name", TextBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('One Record Added');</script>");
                TextBox1.Text = "";
                ShowGrid();
            }
        }

        public void ShowGrid()
        {
            SqlConnection conn = new SqlConnection(str);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tb_category", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ShowGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            ShowGrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Category_ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            SqlConnection con1 = new SqlConnection(str);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM tb_category WHERE Category_ID=@1", con1);
            cmd1.Parameters.AddWithValue("@1", Category_ID);
            cmd1.ExecuteNonQuery();
            con1.Close();
            Response.Write("<script>alert('Category Deleted Successful');</script>");
            ShowGrid();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            ShowGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int Category_ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string Category_Name = (row.FindControl("TextBox2") as TextBox).Text;
            SqlConnection con2 = new SqlConnection(str);
            con2.Open();
            SqlCommand cmd1 = new SqlCommand("UPDATE tb_category SET Category_Name=@1 WHERE Category_ID=@2", con2);
            cmd1.Parameters.AddWithValue("@1", Category_Name);
            cmd1.Parameters.AddWithValue("@2", Category_ID);
            cmd1.ExecuteNonQuery();
            con2.Close();
            Response.Write("<script>alert('Category Updated Successful');</script>");
            GridView1.EditIndex = -1;

        }
    }
}