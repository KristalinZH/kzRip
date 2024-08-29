﻿namespace kzRip.AttackTypes
{
    using SharpCompress.Readers;
    using SharpCompress.Archives;
    using SharpCompress.Archives.Rar;

    using StatusClasses;
    internal static class Attempt
    {
        internal static PasswordResult TryPassword(string path, string password)
        {
            using (RarArchive archive = RarArchive.Open(path, new ReaderOptions() { Password = password }))
            {
                RarArchiveEntry? entry = archive.Entries.FirstOrDefault();

                if (entry == null)
                {
                    return new PasswordResult(true, string.Empty, "The archive is empty!");
                }

                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        entry.WriteTo(stream);
                    }
                }
                catch (Exception)
                {
                    return new PasswordResult(false, password, "Invalid password!");
                }
            }

            return new PasswordResult(true, password, "Password found!");
        }
        internal static bool ArchiveExists(string path)
        {
            try
            {
                using (RarArchive archive = RarArchive.Open(path)) { }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
