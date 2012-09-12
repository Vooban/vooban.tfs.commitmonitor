using System;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsCommitMonitor.SourceControl
{
    public class TfsCheckin
    {
        const string USER_MAPPING_FILENAME = "usernameMapping.bin";
        private static readonly StringDictionary _usernameMappings;

        static TfsCheckin() 
        {
            if (File.Exists(USER_MAPPING_FILENAME))
            {
                var formatter = new BinaryFormatter();
                using(var fs = new FileStream(USER_MAPPING_FILENAME, FileMode.Open))
                    _usernameMappings = (StringDictionary)formatter.Deserialize(fs);
            }
            else
                _usernameMappings = new StringDictionary();            
        }

        public string Comment { get; set; }

        public string Committer { get; set; }

        public string CommitterDomainName { get; set; }

        public DateTime CreationDate { get; set; }

        public int ChangesetId { get; set; }

        public string Owner { get; set; }

        public string ServerId { get; set; }

        public string FolderPath { get; set; }

        public string FolderId { get; set; }

        public string TeamProjectCollection { get; set; }

        public Change[] Changes { get; set; }

        public TfsCheckin(Changeset source, string serverId, string teamProjectCollection, string folderId, string folderPath)
        {
            Comment = source.Comment;
            Committer = GetFullName(source.Committer);
            CommitterDomainName = source.Committer;
            CreationDate = source.CreationDate;
            ChangesetId = source.ChangesetId;
            Owner = source.Owner;
            ServerId = serverId;
            TeamProjectCollection = teamProjectCollection;
            FolderId = folderId;
            FolderPath = folderPath;
        }

        private static string GetFullName(string username)
        {
            try
            {
                if (_usernameMappings.ContainsKey(username))
                    return _usernameMappings[username];

                var de = new DirectoryEntry("WinNT://" + username.Replace("\\", "/"));
                var fullname = de.Properties["fullName"].Value.ToString();
                var parts = fullname.Split(',');

                if (parts.Length >= 2)
                {
                    var correctedName = string.Format("{0} {1}", parts[1].Trim(), parts[0].Trim());                    
                    fullname = correctedName;
                }
                
                _usernameMappings.Add(username, fullname);

                var formatter = new BinaryFormatter();
                using(var fs = new FileStream(USER_MAPPING_FILENAME, FileMode.OpenOrCreate))
                    formatter.Serialize(fs, _usernameMappings);
                
                return fullname;
            }
            catch { return username; }
        }
    }
}
