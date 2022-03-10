using System;

namespace FireSource
{
    public class FireDataSourceFactory : IFactory
    {
        private readonly string FIREBASE_PROJECT_ID = "fire-students-89ad3";
        private readonly string FIREBASE_CREDENTIALS_PATH = "Resources/fire-students-89ad3-firebase-adminsdk-bsgu6-fc42f76265.json";

        public IDataSource CreateDataSource(
            string Collection, 
            string Key)     
        {
            return new FireDataSource(
                FIREBASE_PROJECT_ID,
                Collection,
                Key,
                FIREBASE_CREDENTIALS_PATH);
        }
    }
}