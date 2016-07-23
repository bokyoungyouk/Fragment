﻿using UnityEngine;
using System.Collections;

public class MyNigro : MonoBehaviour {

        public Color _color = Color.yellow;
        public float _radius = 0.1f;

    void OnDrawGizmos()
    {
        //기즈모 색상 설정
        Gizmos.color = _color;
        //구체 모양의 기즈모 생성 DrawSphere(생성위치,반지름)
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
