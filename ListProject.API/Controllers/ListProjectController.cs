using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Numerics;

namespace ListProject.API.Controllers

    
{
   
    [Route("api")]
    public class ListProjectController : Controller
    {


        private string token = "e4593TOKEN";
        private string baseurl = "https://challenge.test.com/";

        [HttpGet]
        public async Task<ActionResult<string>> GetTodoList()
        {
            try
            {
                using (var client = new HttpClient())
                {

                    string endpoint = baseurl + $"todos";


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    var statuscode = response.StatusCode;

                    if (response.IsSuccessStatusCode == true)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        return Ok(data);
                    }
                    else
                    {

                        return StatusCode((int)statuscode, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //first_code HELLO
        [HttpGet]
        [Route("search/{first_code}")]
        public async Task<ActionResult<string>> SearchData(string first_code)
        {
            try
            {

                using (var client = new HttpClient())
                {

                    string endpoint = baseurl + $"todos/search?query={first_code}";


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    var statuscode = response.StatusCode;

                    if (response.IsSuccessStatusCode == true)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        return Ok(data);
                    }
                    else
                    {

                        return StatusCode((int)statuscode, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //second_code=42
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<string>> DeleteData(int second_code)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    string endpoint = baseurl + $"todos?id=42";


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.DeleteAsync(endpoint);
                    var statuscode = response.StatusCode;

                    if (response.IsSuccessStatusCode == true)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        return Ok(data);
                    }
                    else
                    {

                        return StatusCode((int)statuscode, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("Complete")]
        public async Task<string> CompleteChallenge()
        {
            string finalCode = "a";

            string zipFilePath = @"C:\Users\smybl\OneDrive\Belgeler\ListProjectDescription.zip";
            string token = "e4593748f65ba3915c7c06fdda0a5060";
            string url = "https://challenge.photier.com/complete";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                while (finalCode != "zzzzzzz") // Loop until finalCode reaches 'zzzzzzz'.
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(new StringContent(finalCode), "final_code");

                    byte[] zipFileBytes = System.IO.File.ReadAllBytes(zipFilePath);
                    ByteArrayContent fileContent = new ByteArrayContent(zipFileBytes);
                    content.Add(fileContent, "file", zipFilePath);

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return $"Challenge completed successfully! {responseContent} {finalCode}";
                    }

                    finalCode = GenerateNextCode(finalCode); // Generate the next code.
                }
            }

            return "Challenge completion unsuccessful.";
        }

        private string GenerateNextCode(string currentCode)
        {
            char[] codeChars = currentCode.ToCharArray();
            for (int i = codeChars.Length - 1; i >= 0; i--)
            {
                if (codeChars[i] < 'z')
                {
                    codeChars[i]++;
                    return new string(codeChars);
                }
                else
                {
                    codeChars[i] = 'a';
                }
            }
            return new string(codeChars);
        }

    }





}



    


