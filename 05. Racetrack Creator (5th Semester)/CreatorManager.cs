using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorManager : MonoBehaviour
{
    public GameObject startingLine;
    public GameObject creator;

    //**IMPORTANT NOTE**//
    //If another camera is implemented into scene, delete creatorCamera amd topDownCamera GameObject and all instances of its rotation being altered afterward
    public GameObject creatorCamera;
    public GameObject topDownCamera;

    public GameObject trackPiecesFolder;
    public List<GameObject> listOfTrackPieces = new List<GameObject>();

    public Text directionText;

    [SerializeField] private GameObject _settingsSelected;
    private Vector3 creatorStart;
    private Vector3 creatorRotateStart;
    private bool creatorCoRunning = false;
    private bool creatorCoRotateRunning = false;
    private float creatorTimeToLERP = 0.5f;
    private float creatorTimeToRotateLERP = 1.5f;
    private Vector3 topDownStart;
    private bool topDownCoRunning = false;
    private float topDownTimeToLERP = 0.5f;
    private string trackName;
    public enum direction { North, East, South, West };
    public direction trackDirection;

    private void Awake()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_settingsSelected);
    }

    private void Start()
    {
        listOfTrackPieces.Add(startingLine);
        topDownStart = new Vector3(0f, 640f, -60f);
        //Starting line direction faces North
        trackDirection = direction.North;
    }

    private void Update()
    {
        DirectionText();

        if (!creatorCoRunning)
        {
            creatorStart = new Vector3(creatorCamera.transform.position.x, creatorCamera.transform.position.y, creatorCamera.transform.position.z);
        }

        if (!creatorCoRotateRunning)
        {
            creatorRotateStart = new Vector3(creatorCamera.transform.eulerAngles.x, creatorCamera.transform.eulerAngles.y, creatorCamera.transform.eulerAngles.z);
        }

        if (!topDownCoRunning)
        {
            topDownStart = new Vector3(topDownCamera.transform.position.x, 640f, topDownCamera.transform.position.z);
        }
    }

    IEnumerator topDownCamLerpPosition(Vector3 endPos, float duration)
    {
        yield return new WaitForEndOfFrame();

        if (topDownCoRunning)
        {
            yield return new WaitForSeconds(duration);
        }

        topDownCoRunning = true;
        float time = 0;

        while (time < duration)
        {
            topDownCamera.transform.position = Vector3.Lerp(topDownStart, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        topDownCamera.transform.position = endPos;
        topDownCoRunning = false;
    }

    IEnumerator creatorCamLerpPosition(Vector3 endPos, float duration)
    {
        yield return new WaitForEndOfFrame();

        if (creatorCoRunning)
        {
            yield return new WaitForSeconds(duration);
        }

        creatorCoRunning = true;
        float time = 0;

        while (time < duration)
        {
            creatorCamera.transform.position = Vector3.Lerp(creatorStart, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        creatorCamera.transform.position = endPos;
        creatorCoRunning = false;
    }

    IEnumerator creatorCamLerpRotation(Vector3 endPos, float duration)
    {
        yield return new WaitForEndOfFrame();

        if (creatorCoRotateRunning)
        {
            yield return new WaitForSeconds(duration);
        }

        creatorCoRotateRunning = true;
        float time = 0;

        while (time < duration)
        {
            creatorCamera.transform.eulerAngles = Vector3.Slerp(creatorRotateStart, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        creatorCamera.transform.eulerAngles = endPos;
        creatorCoRotateRunning = false;
    }

    //Tells player which direction they are placing a track in
    public void DirectionText()
    {
        directionText.text = "You are traveling " + trackDirection.ToString();
    }

    public void CameraChange()
    {
        if (creatorCamera.activeInHierarchy)
        {
            creatorCamera.SetActive(false);
            topDownCamera.SetActive(true);
        }

        else
        {
            topDownCamera.SetActive(false);
            creatorCamera.SetActive(true);
        }
    }


    /*If more than these GameObjects are added to Racetrack Creator, 
    make a list of GameObjects instead and assign proper index values to 'if' statements in PlaceTrack*/
    public void PlaceTrack(GameObject selectedTrack)
    {
        #region RoadMid200cm
        if (selectedTrack.name == "RoadMid200cm")
        {
            //Starting from the beginning, Starting Line
            if (listOfTrackPieces.Count == 1)
            {
                listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, new Vector3(0f, 0f, 0f), new Quaternion(0f, 180f, 0f, 0f)));
            }

            //Goes through each type of track and transforms position and rotation appropriately according to last track placed, as well as current direction
            else if (trackDirection == direction.North)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }
            }

            else if (trackDirection == direction.East)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }
            }

            else if (trackDirection == direction.South)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
            }

            else if (trackDirection == direction.West)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }
            }
        }
        #endregion

        #region RoadMid400cm
        if (selectedTrack.name == "RoadMid400cm")
        {
            //Starting from the beginning, Starting Line
            if (listOfTrackPieces.Count == 1)
            {
                listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, new Vector3(0f, 0f, 0f), new Quaternion(0f, 180f, 0f, 0f)));
            }

            //Goes through each type of track and transforms position and rotation appropriately according to last track placed, as well as current direction
            else if (trackDirection == direction.North)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                         listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                         listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f)), //New track z-coordinate
                         listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }
            }

            else if (trackDirection == direction.East)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }
            }

            else if (trackDirection == direction.South)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
            }

            else if (trackDirection == direction.West)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }
            }
        }
        #endregion

        #region RoadArcHalfMedR
        if (selectedTrack.name == "RoadArcHalfMedR")
        {
            //Starting from the beginning, Starting Line
            if (listOfTrackPieces.Count == 1)
            {
                listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, new Vector3(0f, 0f, 0f), new Quaternion(0f, -180f, 0f, 0f)));

                trackDirection = direction.East;
            }

            //Goes through each type of track and transforms position and rotation appropriately according to last track placed, as well as current direction
            else if (trackDirection == direction.North)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                trackDirection = direction.East;
            }

            else if (trackDirection == direction.East)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                trackDirection = direction.South;
            }

            else if (trackDirection == direction.South)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                trackDirection = direction.West;
            }

            else if (trackDirection == direction.West)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                trackDirection = direction.North;
            }
        }
        #endregion

        #region RoadArcHalfMedL
        if (selectedTrack.name == "RoadArcHalfMedL")
        {
            //Starting from the beginning, Starting Line
            if (listOfTrackPieces.Count == 1)
            {
                listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, new Vector3(0f, 0f, 0f), new Quaternion(0f, -180f, 0f, 0f)));

                trackDirection = direction.West;
            }

            //Goes through each type of track and transforms position and rotation appropriately according to last track placed, as well as current direction
            else if (trackDirection == direction.North)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                trackDirection = direction.West;
            }

            else if (trackDirection == direction.East)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, -90f, 0f);
                }

                trackDirection = direction.North;
            }

            else if (trackDirection == direction.South)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 150f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }

                trackDirection = direction.East;
            }

            else if (trackDirection == direction.West)
            {
                if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid200cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadMid400cm(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 200f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedR(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                else if (listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL" || listOfTrackPieces[listOfTrackPieces.Count - 1].name == "RoadArcHalfMedL(Clone)")
                {
                    listOfTrackPieces.Add(Instantiate<GameObject>(selectedTrack, (new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 150f, //New track x-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y, //New track y-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 200f)), //New track z-coordinate
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.rotation)); //New track rotation

                    //Update new piece rotation based on appropriate turn direction
                    listOfTrackPieces[listOfTrackPieces.Count - 1].transform.eulerAngles = new Vector3(0f, 90f, 0f);
                }

                trackDirection = direction.South;
            }
        }
        #endregion

        CameraPlacement(listOfTrackPieces[listOfTrackPieces.Count - 1], false);

        //Add track to Track Pieces empty GameObject 'folder' as a child
        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.parent = trackPiecesFolder.transform;
    }

    private void CameraPlacement(GameObject selectedTrack, bool delete)
    {
        //If new track was added
        if (!delete)
        {
            /*if ((listOfTrackPieces.Count % 4) == 0 && (topDownCamera.transform.position.y + 100f) < 1000f)
            {
                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x, topDownCamera.transform.position.y + 100f, topDownCamera.transform.position.z), topDownTimeToLERP));
            }*/

            //Camera facing direction
            if (trackDirection == direction.North)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x, topDownCamera.transform.position.y, topDownCamera.transform.position.z + 150f), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 0f, 0f), creatorTimeToRotateLERP));
            }

            else if (trackDirection == direction.East)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x + 150f, topDownCamera.transform.position.y, topDownCamera.transform.position.z), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 90f, 0f), creatorTimeToRotateLERP));
            }

            else if (trackDirection == direction.South)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x, topDownCamera.transform.position.y, topDownCamera.transform.position.z - 150f), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 180f, 0f), creatorTimeToRotateLERP));
            }

            else if (trackDirection == direction.West)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x - 150f, topDownCamera.transform.position.y, topDownCamera.transform.position.z), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, -90f, 0f), creatorTimeToRotateLERP));
            }
        }

        //If track was deleted
        else if (delete && listOfTrackPieces.Count > 2)
        {
            /*if ((listOfTrackPieces.Count % 4) == 0 && (topDownCamera.transform.position.y + 100f) < 1000f)
            {
                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x, topDownCamera.transform.position.y - 100f, topDownCamera.transform.position.z), topDownTimeToLERP));
            }*/

            //Camera facing direction
            if (trackDirection == direction.North)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z - 100f), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x, topDownCamera.transform.position.y, topDownCamera.transform.position.z - 150f), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 0f, 0f), creatorTimeToRotateLERP));
            }

            else if (trackDirection == direction.East)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x - 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x - 150f, topDownCamera.transform.position.y, topDownCamera.transform.position.z), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 90f, 0f), creatorTimeToRotateLERP));
            }

            else if (trackDirection == direction.South)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 50f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z + 100f), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x, topDownCamera.transform.position.y, topDownCamera.transform.position.z + 150f), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 180f, 0f), creatorTimeToRotateLERP));
            }

            else if (trackDirection == direction.West)
            {
                //Camera position above track according to current track direction path
                if (selectedTrack.name == "RoadMid200cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadMid400cm(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 50f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedR(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                if (selectedTrack.name == "RoadArcHalfMedL(Clone)")
                {
                    StartCoroutine(creatorCamLerpPosition(new Vector3(listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.x + 100f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.y + 265f,
                        listOfTrackPieces[listOfTrackPieces.Count - 1].transform.position.z), creatorTimeToRotateLERP));
                }

                StartCoroutine(topDownCamLerpPosition(new Vector3(topDownCamera.transform.position.x + 150f, topDownCamera.transform.position.y, topDownCamera.transform.position.z), topDownTimeToLERP));
                StartCoroutine(creatorCamLerpRotation(new Vector3(70f, -90f, 0f), creatorTimeToRotateLERP));
            }
        }

        //If starting line is only track left
        else if (delete && listOfTrackPieces.Count == 2)
        {
            StartCoroutine(creatorCamLerpPosition(new Vector3(0f, 265f, -60f), creatorTimeToLERP));
            StartCoroutine(creatorCamLerpRotation(new Vector3(70f, 0f, 0f), creatorTimeToRotateLERP));
            StartCoroutine(topDownCamLerpPosition(new Vector3(0f, 640f, -60f), topDownTimeToLERP));
        }
    }

    public void DeleteTrack()
    {
        //If starting line is last track piece after deleting one more
        if (listOfTrackPieces.Count == 2)
        {
            CameraPlacement(listOfTrackPieces[0], true);

            Destroy(listOfTrackPieces[listOfTrackPieces.Count - 1]);
            listOfTrackPieces.RemoveAt(listOfTrackPieces.Count - 1);
            return;
        }

        //Regular delete statement
        else if (listOfTrackPieces.Count > 2)
        {
            CameraPlacement(listOfTrackPieces[listOfTrackPieces.Count - 1], true);

            Destroy(listOfTrackPieces[listOfTrackPieces.Count - 1]);
            listOfTrackPieces.RemoveAt(listOfTrackPieces.Count - 1);
        }
    }

    public void SaveTrack()
    {
        /*JSON USAGE ONLY
        //To do: Enter new name for track with popup dialogue box
        //JSONTest.instance.SaveTrack(trackName); */
        Destroy(creator);
        TrackCombiner.StartCombining();
        TrackManager.Instance.SpawnPlayersCreator(GameManager.Instance.Players);
    }
}