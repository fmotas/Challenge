using System.Collections.Generic;

using Challenge.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.JsonPatch;

namespace Challenge.Controllers
{
    [Route("api/Items")]
    public class ItemAPIController : Controller
    {
        DataAccess objds;

        public ItemAPIController()
        {
            objds = new DataAccess();
        }

        [HttpGet]
        public IEnumerable<Item> Get() // SHOW ALL ITEMS
        {
            return objds.GetItems();
        }
        [HttpGet("{id:length(24)}")] // SHOW AN ITEM WITH AN SPECIFIC ID
        public IActionResult Get(string id)
        {
            var item = objds.GetItem(new ObjectId(id));
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("FreeItems")] // SHOW ALL FREE ITEMS
        public IActionResult Get(char state)
        {
            var item = objds.GetItem('y');
            if (item == null)
            {
                return NoContent();
            }
            return new ObjectResult(item);
        }
        [HttpGet("LockedItems")] // // SHOW ALL LOCKED ITEMS
        public IActionResult Get(string state, char test)
        {
            var item = objds.GetItem('n');
            if (item == null)
            {
                return NoContent();
            }
            return new ObjectResult(item);
        }




        [HttpPost]
        public IActionResult Post([FromBody]Item i) //ADD A NEW ITEM
        {
            foreach (PropertyInfo prop in typeof(Item).GetProperties())
            {
                if (prop.GetValue(i).ToString() == "0" || prop.GetValue(i) == null)
                {
                    return BadRequest();
                }
            }
            objds.Create(i);
            return new OkObjectResult(i);
        }

        [HttpPut("{id:length(24)}")] //UPDATE AN EXISTING ITEM
        public IActionResult Put(string id, [FromBody]Item i)
        {
            var recId = new ObjectId(id);
            var item = objds.GetItem(recId);
            if (item == null)
            {
                return NotFound();
            }
            if (item.ItemInUse == 'y' & item.ItemFloor != i.ItemFloor)
            {
                return BadRequest();
            }

            objds.Update(recId, i);
            return new OkResult();
        }

       


        [HttpDelete("{id:length(24)}")] // DELETE AN EXISTING ITEM
        public IActionResult Delete(string id)
        {
            var item = objds.GetItem(new ObjectId(id));
            if (item == null)
            {
                return NotFound();
            }

            objds.Remove(item.Id);
            return new OkResult();
        }
        
    }
}