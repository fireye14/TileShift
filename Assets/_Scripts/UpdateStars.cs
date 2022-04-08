using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class UpdateStars : MonoBehaviour
{
    // This script is attached to the level button prefab; used to hold components for more convenient access

	public Image[] stars;
	public Text perfectText;
    public Text levelText;	
}
