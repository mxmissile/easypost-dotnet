using System.Net.Http;

namespace Easypost.Internal
{
    internal interface IEncodable
    {
        FormUrlEncodedContent AsFormUrlEncodedContent();
    }
}