using System;
using System.Collections;
using UnityEngine;

namespace Swift_Blade.Combat.Feedback
{
    public class FlashFeedback : Feedback
    {
        [Header("Flash info")] 
        [SerializeField] private Transform root;
        
        [Range(0f,5f)][SerializeField] private float flashDuration;
        [Range(1,10)][SerializeField] private int flashCount;
        
        [SerializeField] private Material _flashMat;
        [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;
        private Material[] _originMats;

        private WaitForSeconds _flashDuration;
        
        private void Start()
        {
            _meshRenderers = root.GetComponentsInChildren<SkinnedMeshRenderer>();
            _originMats = Array.ConvertAll(_meshRenderers, mesh => mesh.material);
            
            float waitTime = flashDuration / (flashCount * 2);
            _flashDuration = new WaitForSeconds(waitTime);
        }
        
        public override void PlayFeedback()
        {
            StartCoroutine(FlashRoutine());
        }

        public override void ResetFeedback()
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material = _originMats[i];
            }
        }

        private IEnumerator FlashRoutine()
        {
            for (int i = 0; i < flashCount; i++)
            {
                SetMaterials(_flashMat);
                yield return _flashDuration;
                ResetFeedback();
                yield return _flashDuration;
            }
        }

        private void SetMaterials(Material mat)
        {
            foreach (var renderer in _meshRenderers)
            {
                renderer.material = mat;
            }
        }
    }
}