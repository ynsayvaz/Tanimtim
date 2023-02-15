using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tanimtim.WebUI.Models.Entities;

namespace Tanimtim.WebUI.Controllers
{
    public class HizmetlerimizController : Controller
    {
        private const string BaglantiCumlesi = "Server=.;Database=tanitimDB;Integrated security=True;";

        // GET: Hizmetlerimiz
        public ActionResult Index()
        {
            //ado.net ile select işlemi
            List<Hizmetlerimiz> hizmetlerList = new List<Hizmetlerimiz>();
            SqlConnection con = new SqlConnection(BaglantiCumlesi);
            if (con.State == System.Data.ConnectionState.Closed || con.State == System.Data.ConnectionState.Broken)
                con.Open();
            SqlCommand com = new SqlCommand("Select * from Hizmetlerimiz", con);
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                Hizmetlerimiz hizmet = HizmetVerilerYukle(read);
                hizmetlerList.Add(hizmet);
            }
            read.Dispose();
            read.Close();
            con.Close();
            return View(hizmetlerList);
        }

        public ActionResult Update(int? id)
        {
            //ado.net ile update işlemi(procedure kullanmadan)
            if (id == null)
            {
                return Redirect("Index");
            }
            Hizmetlerimiz hizmet = null;
            SqlConnection con = new SqlConnection(BaglantiCumlesi);
            if (con.State == System.Data.ConnectionState.Closed || con.State == System.Data.ConnectionState.Broken)
                con.Open();
            SqlCommand com = new SqlCommand("Select * from Hizmetlerimiz Where ID = " + (int)id, con);
            SqlDataReader read = com.ExecuteReader();
            if (read.Read())
            {
                hizmet = HizmetVerilerYukle(read);
            }
            read.Dispose();
            read.Close();
            con.Close();
            return View(hizmet);
        }
        //[HttpPost]
        //public ActionResult Update(Hizmetlerimiz model, HttpPostedFileBase resimUrl)
        //{

        //    //ado.net ile procedure kullanarak(parametreli) update işlemi
        //    SqlConnection con = new SqlConnection(BaglantiCumlesi);
        //    if(con.State == System.Data.ConnectionState.Closed || con.State == System.Data.ConnectionState.Broken)
        //    {
        //        con.Open();
        //    }
        //    SqlCommand com = new SqlCommand("SP_HIZMETLERUPDATE",con);
        //    com.CommandType = System.Data.CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@Id", model.Id);
        //    com.Parameters.AddWithValue("@Baslik", model.Baslik);
        //    com.Parameters.AddWithValue("@Icerik", model.Icerik);
        //    com.Parameters.AddWithValue("@ResimUrl", model.ResimUrl);
        //    int etkilenenSatir = com.ExecuteNonQuery();
        //    con.Close();


        //    return Redirect("Index");
        //}
        private static Hizmetlerimiz HizmetVerilerYukle(SqlDataReader read)
        {
            Hizmetlerimiz hizmet = null;
            try
            {
                hizmet = new Hizmetlerimiz();
                hizmet.Id = read.GetInt32(0);
                hizmet.Baslik = Convert.ToString(read["Baslik"]);
                hizmet.Icerik = read.GetString(2);
                hizmet.ResimUrl = read.GetString(3);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return hizmet;
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(Hizmetlerimiz hizmet)
        {
            int etkilenenSatir = 0;
            SqlConnection con = new SqlConnection(BaglantiCumlesi);
            if (con.State == System.Data.ConnectionState.Closed || con.State == System.Data.ConnectionState.Broken)
                con.Open();
            //SqlCommand com = new SqlCommand("SP_HIZMETLEREKLE", con);
            hizmet.ResimUrl = "";
            SqlCommand com = new SqlCommand();
            com.CommandText = "SP_HIZMETLEREKLE";
            com.Connection = con;
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Baslik", hizmet.Baslik);
            com.Parameters.AddWithValue("@Icerik", hizmet.Icerik);
            com.Parameters.AddWithValue("@ResimUrl", hizmet.ResimUrl);
            try
            {
                etkilenenSatir = com.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {

                throw new Exception(sqlEx.Message);
            }
            finally
            {
                con.Close();
            }
            return Redirect("Index");
        }
    }
}