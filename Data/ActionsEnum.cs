using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Buecherwurm.Data
{
    public class Actions
    {
        public enum Type
        {
            catalogueGet,
            cataloguePost,
        }
    }
}
