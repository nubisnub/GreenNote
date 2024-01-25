using GreenNote.Web.Models;
using GreenNote.Web.Models.StudentNameList;
using GreenNote.Web.Services.IServices;
using GreenNote.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GreenNote.Web.Controllers
{
    public class ClassInfoController : Controller
    {
        private readonly IClassInfoService _classInfoService;
        private readonly StudentNamelist _studentNameList;

        public ClassInfoController(IClassInfoService classInfoService)
        {
            _classInfoService = classInfoService;
            _studentNameList = new StudentNamelist();
            
        }

        public async Task<IActionResult> ClassInfoAllView()
        {
            List<ClassInfoDto> list = new();
            ResponseDto response = await _classInfoService.GetAllAsync();

            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<ClassInfoDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            var studentNameList = _studentNameList.NameList;



            ViewBag.StudentNameList = studentNameList;

            return View(list);
        }

        public async Task<IActionResult> ClassInfoDetail(int classInfoId)
        {
            ResponseDto response = await _classInfoService.GetOneDataAsync(classInfoId);
            ClassInfoDto classInfoDto = new();
            classInfoDto = JsonConvert.DeserializeObject<ClassInfoDto>(Convert.ToString(response.Result));
            return View(classInfoDto);
        }



        public async Task<IActionResult> ClassInfoIndex(string studentName)
        {
            if (studentName == null)
            {
                List<ClassInfoDto> list = new();
                ResponseDto response = await _classInfoService.GetAllAsync();

                if (response != null && response.IsSuccess == true)
                {
                    list = JsonConvert.DeserializeObject<List<ClassInfoDto>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response.Message;
                }

                //비효율적
                var studentNameList = _studentNameList.NameList;
                ViewBag.StudentNameList = studentNameList;

                return View(list);
            }
            else
            {
                List<ClassInfoDto> list = new();
                ResponseDto response = await _classInfoService.GetStudentAsync(studentName);

                if (response != null && response.IsSuccess == true)
                {
                    list = JsonConvert.DeserializeObject<List<ClassInfoDto>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response.Message;
                }

                var studentNameList = _studentNameList.NameList;


                ViewBag.StudentNameList = studentNameList;

                return View(list);
            }
        }


        public async Task<IActionResult> ClassInfoCreate()
        {
            //비효율적
            var studentNameList = _studentNameList.NameList;
            ViewBag.StudentNameList = studentNameList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClassInfoCreate(ClassInfoDto classInfoDto)
        {
            

            if (ModelState.IsValid)
            {
                ResponseDto? response = await _classInfoService.CreateAsync(classInfoDto);

                if (response != null && response.IsSuccess == true)
                {
                    TempData["Success"] = "ClassInfo Created Successfully";
                    return RedirectToAction(nameof(ClassInfoIndex));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClassInfoDelete(int id)
        {
            ResponseDto? response = await _classInfoService.DeleteAsync(id);
            TempData["Success"] = "Delete a ClassInfo";
            return RedirectToAction(nameof(ClassInfoIndex));
        }

        [HttpPost]
        public async Task<IActionResult> ClassInfoUpdate(ClassInfoDto classInfoDto)
        {
            if (ModelState.IsValid)
            {
            ResponseDto? response = await _classInfoService.UpdateAsync(classInfoDto);
                if (response != null && response.IsSuccess == true)
                {
                TempData["Success"] = "Update is Success";
                return RedirectToAction(nameof(ClassInfoIndex));

                }
                else
                {
                TempData["Error"] = response.Message;
                    return View();
                }
            }
            else
            {
            return View();
            }
        }
    }
}
