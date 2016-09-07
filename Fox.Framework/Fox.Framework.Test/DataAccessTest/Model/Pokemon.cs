using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.Test.DataAccessTest.Model
{
    public class Pokemon
    {
        [DataMapping("Id")]
        public int Id { get; set; }

        [DataMapping("Name")]
        public string Name { get; set; }
    }
}
