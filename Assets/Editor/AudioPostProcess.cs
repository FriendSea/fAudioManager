using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioPostProcess : AssetPostprocessor
{
    private void OnPreprocessAudio()
    {
        var importer = assetImporter as AudioImporter;
        importer.defaultSampleSettings = new AudioImporterSampleSettings()
        {
            compressionFormat = AudioCompressionFormat.Vorbis,
            loadType = AudioClipLoadType.CompressedInMemory,
            quality = 1f
        };
    }
}
