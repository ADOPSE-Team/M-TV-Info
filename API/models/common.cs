using System;
using System.Collections.Generic;

namespace API.Models 
{
    public static class ModelsCommon 
    {
        private static Dictionary<Type, string> TypeToTableDictionary = new Dictionary<Type, string> {
            { typeof(FAVOURITE_model), "FAVOURITE" },    
            { typeof(RATING_model), "RATING" },    
            { typeof(ROLE_model), "ROLES" },
            { typeof(MEDIA_LOOKUP_model), "MEDIA_LOOKUP" },
            { typeof(USER_model), "USERS" },
            { typeof(WATCHLIST_ITEM_model), "WATCHLIST_ITEM" },
            { typeof(WATCHLIST_model), "WATCHLIST" } 
        };

        public enum MediaType 
        {
            Movie,
            TV_Show
        };

        public static string TypeToTable<T>(){
            // if (typeof(FAVOURITE_model).IsAssignableFrom(typeof(T))) return "FAVOURITE";
            // else if (typeof(RATING_model).IsAssignableFrom(typeof(T))) return "RATING";
            // else if (typeof(ROLE_model).IsAssignableFrom(typeof(T))) return "ROLES";
            // else if (typeof(MEDIA_LOOKUP_model).IsAssignableFrom(typeof(T))) return "TV_LOOKUP";
            // else if (typeof(USER_model).IsAssignableFrom(typeof(T))) return "USERS";
            // else if (typeof(WATCHLIST_ITEM_model).IsAssignableFrom(typeof(T))) return "WATCHLIST_ITEM";
            // else if (typeof(WATCHLIST_model).IsAssignableFrom(typeof(T))) return "WATCHLIST";
            // else return "";

            string description = "";
            TypeToTableDictionary.TryGetValue(typeof(T), out description);
            return description;
        }

    }

}