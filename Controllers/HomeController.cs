using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using IsTakip.Models;
using Microsoft.Data.SqlClient;

namespace IsTakip.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "Select * From jobs";
        var isler = connection.Query<JobModel>(sql).ToList();
        
        return View(isler);
    }

    public IActionResult Rapor()
    {
        return View();
    }
}