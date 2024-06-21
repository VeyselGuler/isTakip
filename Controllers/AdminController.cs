using Dapper;
using IsTakip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IsTakip.Controllers;

public class AdminController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "SELECT * FROM jobs";
        var isler = connection.Query<JobModel>(sql).ToList();
        
        return View(isler);
    }

    [HttpGet]
    public IActionResult AddJob()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddJob(JobModel Model)
    {
        // if(!ModelState.IsValid)
        // {
        //     ViewBag.MessageCssClass = "alert-danger";
        //     ViewBag.Message = "Eksik veya hatalı işlem yaptın.";
        //     return View();
        // }
        Model.CreatedTime = DateTime.Now;
        Model.Durum = "Aktif";
        
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql =
            "INSERT INTO jobs (job,detail,durum,atanankisi,createdtime) VALUES (@job,@detail,@durum,@atanankisi,@createdtime)";
        var data = new
        {
            Model.Job,
            Model.Detail,
            Model.Durum,
            Model.AtananKisi,
            Model.CreatedTime
        };
        var rowEffected = connection.Execute(sql, data);
        
        return RedirectToAction("Index");
    }
    

    public IActionResult Sil(int id)
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "DELETE FROM jobs WHERE id=@id";

        var rowAffected = connection.Execute(sql, new {Id = id});
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult SaveEdit(JobModel model)
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "UPDATE jobs SET Durum = @durum WHERE id = @id";
        var param = new
        {
            model.Id,
            model.Job,
            model.Detail,
            model.Durum,
            model.AtananKisi,
            model.CreatedTime
        };
        var rowAffected = connection.Execute(sql, param);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Duzenle(int id)
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var isler = connection.QuerySingleOrDefault<JobModel>("SELECT * FROM jobs WHERE id=@id", new {Id = id});

        return View(isler);
    }

    [HttpPost]
    public IActionResult Duzenle(JobModel model)
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "UPDATE jobs SET job=@job, atanankisi= @atanankisi, detail= @detail WHERE id = @id";

        var param = new
        {
            model.Id,
            model.Job,
            model.Detail,
            model.AtananKisi,
            model.Durum,
            model.CreatedTime
        };
        var rowAffected = connection.Execute(sql, param);
        return RedirectToAction("Index");
    }
}