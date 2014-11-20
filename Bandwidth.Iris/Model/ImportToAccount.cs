using System.Linq;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    public class ImportToAccount
    {
        private const string PortOutPath = "importToAccounts";

        public static async Task<Note> AddNote(Client client, string orderId, Note note)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(string.Format("{0}/{1}/notes", PortOutPath, orderId)), note))
            {
                var list = await GetNotes(client, orderId);
                var id = client.GetIdFromLocationHeader(response.Headers.Location);
                return list.First(n => n.Id == id);
            }
        }

        public static async Task<Note[]> GetNotes(Client client, string orderId)
        {
            return
                (await
                    client.MakeGetRequest<Notes>(client.ConcatAccountPath(string.Format("{0}/{1}/notes", PortOutPath, orderId))))
                    .List;
        }
#if !PCL
        public static Task<Note> AddNote(string orderId, Note note)
        {
            return AddNote(Client.GetInstance(), orderId, note);
        }

        public static Task<Note[]> GetNotes(string orderId)
        {
            return GetNotes(Client.GetInstance(), orderId);
        }
#endif
    }
}
