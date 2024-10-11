using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Settings/GameParameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private float simpleSpeed = 5f;
    [SerializeField] private float hardSpeed = 10f;
    [SerializeField] private float goalRadius = 1f;
    [SerializeField] private float classicFont = 5f;
    [SerializeField] private float pointsFont = 5f;

    // Public properties to access the values
    public float SimpleSpeed => simpleSpeed / 10f;
    public float HardSpeed => hardSpeed / 10f;
    public float GoalRadius => goalRadius;
    public float ClassicFont => classicFont;
    public float PointsFont => pointsFont;
}