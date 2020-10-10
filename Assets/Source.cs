using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour{
    private float startPosX,startPosY,x;
    private bool isHeld=false;
    private Vector3 resetPosition,resetPosition0;
    void Start(){
        resetPosition=this.transform.localPosition;
        resetPosition0=resetPosition;
    }
    void OnMouseDown(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos;
            mousePos=Input.mousePosition;
            mousePos=Camera.main.ScreenToWorldPoint(mousePos);
            startPosX=mousePos.x-this.transform.localPosition.x;
            startPosY=mousePos.y-this.transform.localPosition.y;
            isHeld=true;
            resetPosition0=this.transform.localPosition;
        }
    }
    void OnMouseUp(){
        isHeld=false;
        x=this.transform.position.x;
        if(x<-5.0){
            resetPosition=new Vector3(GameObject.Find("p1").transform.position.x,this.transform.localPosition.y,0);
        }
        if(x>-3.5&&x<3.5){
            resetPosition=new Vector3(GameObject.Find("p2").transform.position.x,this.transform.localPosition.y,0);
        }
        if(x>5.0){
            resetPosition=new Vector3(GameObject.Find("p3").transform.position.x,this.transform.localPosition.y,0);
        }
        this.transform.localPosition=new Vector3(resetPosition.x,resetPosition.y,0);
    }
    void Update(){
        int layerMask = 1<<gameObject.layer;
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.up,1f,~layerMask);
        if(hit.collider!=null){
            isHeld=false;
        }
        if(isHeld==true){
            Vector3 mousePos;
            mousePos=Input.mousePosition;
            mousePos=Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition=new Vector3(mousePos.x-startPosX,mousePos.y-startPosY,0);
        }
        var l1 = gameObject.transform.TransformPoint(0, 0, 0);
        var l2 = gameObject.transform.TransformPoint(1, 1, 0);
        var w = l2.x - l1.x;
        RaycastHit2D hitd=Physics2D.Raycast(transform.position,Vector2.down,1f,~layerMask);
        if(hitd.collider!=null){
           // Debug.DrawRay(transform.position, Vector2.down*1f, Color.green);
            var select=hitd.transform;
            var p1 = select.transform.TransformPoint(0, 0, 0);
            var p2 = select.transform.TransformPoint(1, 1, 0);
            var h = p2.x - p1.x;
            print(w);
            if(w>h){
                this.transform.localPosition=new Vector3(resetPosition0.x,resetPosition0.y,0);
                return;
            }
        }
    }
}
