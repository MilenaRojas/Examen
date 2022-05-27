using Lab3MVC.Models;
using Lab3MVC.Models.Data;
using Lab3MVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab3MVC.Controllers
{
    public class StudentsController : ControllerBase
    {

        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            IEnumerable<Student> students = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7151/api/students/");
                    var responseTask = client.GetAsync("GetStudents");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Student>>();
                        readTask.Wait();
                        //lee los estudiantes provenientes de la API
                        students = readTask.Result;
                    }
                    else
                    {
                        students = Enumerable.Empty<Student>();
                    }
                }
            }
            catch
            {

                ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator");

            }

            return students;
        }

        // GET api/<StudentsController>/5
        /* [HttpGet("{id}")]
         public string Get(int id)
         {

         }*/

        [HttpGet]
        public Student GetById(int id)
        {
            Student student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7151/api/students/" + id);
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    //lee el estudiante provenientes de la API
                    student = readTask.Result;


                }
            }

            return student;

        }

        [HttpGet]
        public IEnumerable<Student> GetByName(String name)
        {
            IEnumerable<Student> students = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7151/api/students/"+name);
                    var responseTask = client.GetAsync("SelectStudents");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Student>>();
                        readTask.Wait();
                        //lee los estudiantes provenientes de la API
                        students = readTask.Result;
                    }
                    else
                    {
                        students = Enumerable.Empty<Student>();
                    }
                }
            }
            catch
            {

                ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator");

            }

            return students;
        }



        /* // POST api/<StudentsController>
         [HttpPost]
         public async Task<ActionResult<Student>> Post ([FromBody] Student student)
         {
             try
             {
                 using (var client = new HttpClient())
                 {
                     //  client.BaseAddress = new Uri("https://localhost:7151/api/");

                     //   var postTask = client.PostAsJsonAsync("students", student);
                     //   postTask.Wait();

                     /*     string str = JsonConvert.SerializeObject(student);
                          HttpContent httpContent = new StringContent(str, System.Text.Encoding.UTF8, "application/json");
                          await httpContent.LoadIntoBufferAsync();
                          HttpResponseMessage httpResponseMessage = await client.PostAsync("https://localhost:7151/api/students/", httpContent);

                          */

        //   var result = postTask.Result;

        /*  var targeturi = "https://localhost:7151/api/students/";
          var myclient = new System.Net.Http.HttpClient();

          HttpContent content = new FormUrlEncodedContent(new[]
          {
              new KeyValuePair<string, string>("name", student.Name),
              new KeyValuePair<string, string>("dob", "08/05/2005"),
              new KeyValuePair<string, string>("directions", "Take twice daily"),
              new KeyValuePair<string, string>("med-details", "Amox 500mg"),
              new KeyValuePair<string, string>("dry-run", "False")
          });

          content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
          var result = client.PostAsync(targeturi, content).Result;
          string resultContent = await result.Content.ReadAsStringAsync();
        */
        /*     client.BaseAddress = new Uri("https://localhost:7151/api/");
             HttpResponseMessage response = await client.PostAsJsonAsync(
         "students", student);
             client.DefaultRequestHeaders.ExpectContinue = false;
             response.EnsureSuccessStatusCode();

             if (response.IsSuccessStatusCode)
             {
                 return Ok(response);
                 // TODO: return new JsonResult(student);
             }
             else
             {
                 // TODO should be customized to meet the client's needs
                 return Ok(response);
             }
         }

     }
     catch (Exception)
     {

         throw;
     }

 }*/

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7151/api/");

                var postTask = client.PostAsJsonAsync("students", student);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                    // TODO: return new JsonResult(student);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Student student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7151/api/students/");
                var putTask = client.PutAsJsonAsync("PutStudent/" + student.Id, student);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                    // TODO: return new JsonResult(student);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7151/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("students/" + id.ToString());
                //deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return new JsonResult(result);
                }
                else
                {
                    //camino del error
                    return new JsonResult(result);

                }
            }


        }

    }
}
