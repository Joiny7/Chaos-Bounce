using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public MainController main;

	private int playWidth = 8;
	public float distanceBetweenObjects = 0.65f; //for 9/16 aspect, changes according to aspect
	public int rowsSpawned;

	[SerializeField]
	private SpawnHelper helper;
	[SerializeField]
	private RoundController roundz;

	#region Lists of Gameobjects
	public List<GameObject> orbsSpawned = new List<GameObject>();
	public List<GameObject> allEnemies = new List<GameObject>();
	public List<GameObject> powerUps = new List<GameObject>();
	public List<GameObject> allDebris = new List<GameObject>();
	public List<GrowingBrick> growers = new List<GrowingBrick>();
	public List<GrowingDowner> expanders = new List<GrowingDowner>();

	private List<Vector3> spawnPositions = new List<Vector3>();
	private List<Vector3> readyPositions = new List<Vector3>();

	public List<float> spawnPositionsX = new List<float>();

	private float spawnPosY;
	public float spawnPosZ = -10f;
	#endregion

	private Camera mainCamera;
	private StatusController status;
	private PowerUpsController powerUpsController;

	PlayerData playerData;

    public delegate void SpawnerAction ();

    public static event SpawnerAction OneMoreBlockDestroyed;

    public static event SpawnerAction IcePickedUp;
    public static event SpawnerAction MetalPickedUp;
    public static event SpawnerAction FlippyFloopPickedUp;

    private void Start() {
		mainCamera = Camera.main;
		//distanceBetweenObjects *= mainCamera.aspect / (9.0f / 16.0f); // change according changes of aspect ratio
		spawnPosY = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.safeArea.max.y - Screen.height * 0.06f, 0f)).y - distanceBetweenObjects / 2f;
		CreateSpawnPositions();
		Load();

		if (rowsSpawned < 1) {
			for (int i = 0; i < 1; i++) {
				SpawnRow();
			}
		}
	}

	private void Awake() {
		powerUpsController = FindObjectOfType<PowerUpsController>();
		status = FindObjectOfType<StatusController>();
	}

	void CreateSpawnPositions() {
		spawnPositions = new List<Vector3>();

		float screenDistance = (float)Screen.width / (float)playWidth;

        var StartPoint = mainCamera.ScreenToWorldPoint (Vector3.zero);
        var EndPoint = mainCamera.ScreenToWorldPoint (Vector3.right * screenDistance);

        distanceBetweenObjects = Vector3.Distance(EndPoint,StartPoint);

        for (int i = 0; i < playWidth; i++) {
            spawnPositionsX.Add (mainCamera.ScreenToWorldPoint (Vector3.zero).x + distanceBetweenObjects / 2f + distanceBetweenObjects * i);
        }

        for (int i = 0; i < playWidth; i++) {
			spawnPositions.Add(new Vector3(spawnPositionsX[i], spawnPosY, spawnPosZ));
		}
    }

	private void MoveStuff() {
		ExpandIfNeeded();
		MoveEnemies();
		MovePowerUps();
		MoveDebris();
		MoveCollects();
	}

	private void Prepare() {
		readyPositions.Clear();
		readyPositions.AddRange(spawnPositions);
		var collSpotint = Random.Range(0, readyPositions.Count);
		helper.SpawnCollectOrb(readyPositions[collSpotint]);
		var removeSpot = readyPositions[collSpotint];
		readyPositions.Remove(removeSpot);
		main.CurrentScore++;
	}

	private void Spawn() {
		Prepare();

		for (int i = 0; i < readyPositions.Count; i++) {
			var spot = readyPositions[i];

			if (spot != null) {
				if (rowsSpawned <= 50) {
					//Hit Spawn
					if (Random.Range(1, 7) <= 5) {
						roundz.Round1to50(spot);
						readyPositions.Remove(spot);
					}
				}
				//Round 41-60
				if (51 <= rowsSpawned && rowsSpawned <= 100) {
					//Hit Spawn
					if (Random.Range(1, 9) <= 7) {
						roundz.Round51to100(spot);
						readyPositions.Remove(spot);
					}
				}
				//Round 61-100
				if (101 <= rowsSpawned && rowsSpawned <= 150) {
					//Hit Spawn
					if (Random.Range(1, 11) <= 9) {
						roundz.Round101to150(spot);
						readyPositions.Remove(spot);
					}
				}
				//Round 101-150
				if (151 <= rowsSpawned && rowsSpawned <= 200) {
					//Hit Spawn
					if (Random.Range(1, 15) <= 13) {
						roundz.Round151to200(spot);
						readyPositions.Remove(spot);
					}
				}
				if (201 <= rowsSpawned && rowsSpawned <= 250) {
					//Hit Spawn
					if (Random.Range(1, 20) <= 18) {
						roundz.Round201to250(spot);
						readyPositions.Remove(spot);
					}
				}
				if (251 <= rowsSpawned && rowsSpawned <= 300) {
					//Hit Spawn
					if (Random.Range(1, 30) <= 28) {
						roundz.Round251to300(spot);
						readyPositions.Remove(spot);
					}
				}
				if (rowsSpawned > 300) {
					roundz.RoundInfinity(spot);
					readyPositions.Remove(spot);
				}
			}
		}
	}

	public void SpawnRow() {
		MoveStuff();
		DoGrow();
		Spawn();
		rowsSpawned++;
		status.score.text = rowsSpawned.ToString();
		status.SetShotNewRound();
		Save();
	}

	private void DoGrow() {
		var growersInScene = growers;

		for (int i = 0; i < growersInScene.Count; i++) {
			var grow = growersInScene[i];
			if (grow != null) {
				if (grow.roundSpawned != rowsSpawned) {
					grow.Grow();
				}
			} else {
				growers.RemoveAt(i);
			}
		}
	}

	private void MoveEnemies() {
		for (int i = 0; i < allEnemies.Count; i++) {
			var enemy = allEnemies[i];
			if (enemy != null) {
				enemy.transform.position += Vector3.down * distanceBetweenObjects;
			}
		}
	}

	private void MovePowerUps() {
		for (int i = 0; i < powerUps.Count; i++) {
			var up = powerUps[i];
			if (up != null) {
				up.transform.position += Vector3.down * distanceBetweenObjects;
			}
		}
	}

	private void MoveDebris() {
		for (int i = 0; i < allDebris.Count; i++) {
			var deb = allDebris[i];
			if (deb != null) {
				deb.transform.position += Vector3.down * distanceBetweenObjects;
			}
		}
	}

	private void MoveCollects() {
		for (int i = 0; i < orbsSpawned.Count; i++) {
			var col = orbsSpawned[i];
			if (col != null) {
				col.transform.position += Vector3.down * distanceBetweenObjects;
			}
		}
	}

	private void ExpandIfNeeded() {
		if (expanders.Count > 0) {
			for (int i = 0; i < expanders.Count; i++) {
				if (expanders[i] != null) {
					GrowingDowner down = expanders[i];
					down.Grow();
				} else {
					expanders.Remove(expanders[i]);
				}
			}
		}
	}

	public void Save() {
		List<MapObject> mapObjects = new List<MapObject>();

		//if (!main.Losing) {
		BaseObject[] baseObjects = FindObjectsOfType<BaseObject>();

		for (int i = 0; i < baseObjects.Length; i++) {
            if(baseObjects[i] != null) { //temp solution for not saving "broken" objects
                if(spawnPositionsX.IndexOf (baseObjects[i].transform.position.x) == -1) {
                    Debug.LogError ("Save object error (position): " + baseObjects[i].name + " " + baseObjects[i].transform.position);
                }
                MapObject mo = new MapObject (spawnPositionsX.IndexOf (baseObjects[i].transform.position.x), baseObjects[i].transform.position.y, baseObjects[i].hitsRemaining, baseObjects[i].name, (int)baseObjects[i].state);
                mapObjects.Add (mo);
            }
		}
		//}

		SaveSystem.SavePlayer(
				main.UpdateHighScore(), main.CurrentScore,
				mapObjects.ToArray(),
				main.visualOrbEdge.transform.position.x, main.visualOrbEdge.transform.position.y,
				powerUpsController.GetPowerUps(),
				main.launcher.orbs.Count,
				playerData.playerName,
				playerData.userId
				);
	}

	public void Load() {
		playerData = SaveSystem.LoadPlayer();

		if (playerData == null) return;

		MapObject[] mapObjects = playerData.mapObjects;

		//kinda weird loop and switch, that could be changed later :)
		if (mapObjects != null) {
			for (int i = 0; i < mapObjects.Length; i++) {
                if(mapObjects[i].xIndex == -1) {
                    Debug.LogError ("Load object error (position): " + mapObjects[i].name);
                    break;
                }

                Vector3 mapObjectPos = new Vector3(spawnPositionsX[mapObjects[i].xIndex], mapObjects[i].y, spawnPosZ);
				switch (mapObjects[i].name) {
					case "Brick(Clone)":
						helper.SpawnBrick(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "CollectOrb(Clone)":
						helper.SpawnCollectOrb(mapObjectPos);
						break;
					case "Pyramid(Clone)":
						helper.SpawnPyramid(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "RotatingPyramid(Clone)":
						helper.SpawnRotPyramid(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "Ball(Clone)":
						helper.SpawnBall(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "Rhombus(Clone)":
						helper.SpawnRhombus(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "Hex(Clone)":
						helper.SpawnHex(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "GrowingBrick(Clone)":
						helper.SpawnGrowingBrick(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt, mapObjects[i].hitsLeft);
						break;
					case "Pentagram(Clone)":
						helper.SpawnPentagram(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt);
						break;
					case "FlippyFloop(Clone)":
						helper.SpawnFlippyFloop(mapObjectPos);
						break;
					case "Grower(Clone)":
						helper.SpawnGrower(mapObjectPos);
						break;
					case "Shrinker(Clone)":
						helper.SpawnShrinker(mapObjectPos);
						break;
					case "Slowy(Clone)":
						helper.SpawnSlowy(mapObjectPos);
						break;
					case "PlasmaUp(Clone)":
						helper.SpawnPlasma(mapObjectPos);
						break;
					case "LineKiller(Clone)":
						helper.SpawnLineKiller(mapObjectPos);
						break;
					case "FireUp(Clone)":
						helper.SpawnFire(mapObjectPos);
						break;
					case "Mapkiller(Clone)":
						helper.SpawnMapKiller(mapObjectPos);
						break;
					case "Downer(Clone)":
						helper.SpawnDowner(mapObjectPos);
						break;
					case "Doubler(Clone)":
						helper.SpawnDoubler(mapObjectPos);
						break;
					case "Tenfolder(Clone)":
						helper.SpawnTenfolder(mapObjectPos);
						break;
					case "BlackOut(Clone)":
						helper.SpawnBlackOut(mapObjectPos);
						break;
					case "Ooze(Clone)":
						helper.SpawnOoze(mapObjectPos);
						break;
					case "Rotator(Clone)":
						helper.SpawnRotator(mapObjectPos);
						break;
					case "RotatorPlus(Clone)":
						helper.SpawnRotatorPlus(mapObjectPos);
						break;
					case "MiniWall(Clone)":
						helper.SpawnMiniWall(mapObjectPos);
						break;
					case "DownPyramid(Clone)":
						helper.SpawnDownPyramid(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt);
						break;
					case "GrowingDowner(Clone)":
						helper.SpawnGrowingDowner(mapObjectPos);
						break;
					case "TripleShot(Clone)":
						helper.SpawnTripleShot(mapObjectPos);
						break;
					case "HandGrenade(Clone)":
						helper.SpawnHandGrenade(mapObjectPos);
						break;
					case "BigGrenade(Clone)":
						helper.SpawnBigGrenade(mapObjectPos);
						break;
					case "Joker(Clone)":
						helper.SpawnJoker(mapObjectPos, (DamageReductionState)mapObjects[i].damageReductionStateInt);
						break;
					default:
						break;
				}
			}
			rowsSpawned = playerData.currentScore;
		}
	}

    public void BlockDestroyed() {
        if (OneMoreBlockDestroyed != null) {
            OneMoreBlockDestroyed ();
        }
    }

    public void IcePicked () {
        if (IcePickedUp != null) {
            IcePickedUp ();
        }
    }

    public void MetalPicked () {
        if (MetalPickedUp != null) {
            MetalPickedUp ();
        }
    }

    public void FlippyFloopPicked () {
        if (FlippyFloopPickedUp != null) {
            FlippyFloopPickedUp ();
        }
    }
}