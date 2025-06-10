﻿using System.Text.Json;

namespace ProjectHouseWithLeaves.Helper.Session
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            return json == null ? default : JsonSerializer.Deserialize<T>(json);
        }
        public static void RemoveObject(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
