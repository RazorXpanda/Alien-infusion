using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Transform target2;

        [SerializeField]
        private AnimationCurve curve;

        [SerializeField]
        private float duration = 4;

        private void Start()
        {
            if (gameObject.name.Equals("Platform_Top"))
            {
                this.StartCoroutine(this.Lerp(this.duration));
            }
            else
            {
                this.StartCoroutine(this.Lerp2(this.duration));
            }
        }

        private IEnumerator Lerp(float time)
        {
            // wait for 2 seconds before starting the animation.
            // this line does not actually have anything to do with the lerp
            yield return new WaitForSeconds(2);

            var tr = this.transform;
            var startPos = tr.position;
            var endPos = this.target.position;

            var start = Time.time;

            var completion = 0f;
            while(true)
            {
                while (completion < 1)
                {
                    tr.position = Vector3.Lerp(startPos, endPos, this.curve.Evaluate(completion));
                    completion = (Time.time - start) / time;
                    yield return null;
                }

                yield return new WaitForSeconds(3f);
                completion = 0;
                start = Time.time;

                while(completion < 1)
                {
                    tr.position = Vector3.Lerp(endPos, startPos, this.curve.Evaluate(completion));
                    completion = (Time.time - start) / time;
                    yield return null;
                }

                yield return new WaitForSeconds(3f);
                completion = 0;
                start = Time.time;
            }
        }

        private IEnumerator Lerp2(float time)
        {
            // wait for 2 seconds before starting the animation.
            // this line does not actually have anything to do with the lerp
            yield return new WaitForSeconds(2);

            var tr = this.transform;
            var startPos = tr.position;
            var endPos = this.target.position;
            var endPos2 = this.target2.position;

            var start = Time.time;            
            var completion = 0f;
            
            while(true)
            {
                while (completion < 1)
                {
                    tr.position = Vector3.Lerp(startPos, endPos, this.curve.Evaluate(completion));
                    completion = (Time.time - start) / time;
                    yield return null;
                }

                yield return new WaitForSeconds(1f);
                completion = 0;
                start = Time.time;

                while(completion < 1)
                {
                    tr.position = Vector3.Lerp(endPos, endPos2, this.curve.Evaluate(completion));
                    completion = (Time.time - start) / time;
                    yield return null;
                }

                yield return new WaitForSeconds(1f);
                completion = 0;
                start = Time.time;

                while(completion < 1)
                {
                    tr.position = Vector3.Lerp(endPos2, startPos, this.curve.Evaluate(completion));
                    completion = (Time.time - start) / time;
                    yield return null;
                }

                yield return new WaitForSeconds(3f);
                completion = 0;
                start = Time.time;
            }
        }
    }
