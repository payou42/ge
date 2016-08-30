﻿using Newtonsoft.Json;
using System;
using System.IO;
using Veldrid.Assets;

namespace Engine.Assets
{
    public class AssetSystem : GameSystem
    {
        private readonly AssetDatabase _ad;
        private string _assetRootPath;

        public AssetSystem(string assetRootPath, SerializationBinder binder)
        {
            _assetRootPath = assetRootPath;
            _ad = CreateAssetDatabase(binder);
            LooseFileDatabase.AddExtensionTypeMapping(".scene", typeof(SceneAsset));
        }

        protected virtual AssetDatabase CreateAssetDatabase(SerializationBinder binder)
        {
            var fileAssets = new LooseFileDatabase(_assetRootPath);
            fileAssets.DefaultSerializer.Binder = binder;
            var embeddedAssets = new EngineEmbeddedAssets();
            var compoundDB = new CompoundAssetDatabase();
            compoundDB.AddDatabase(fileAssets);
            compoundDB.AddDatabase(embeddedAssets);
            return compoundDB;
        }

        public AssetDatabase Database => _ad;

        protected override void UpdateCore(float deltaSeconds)
        {
        }
    }
}