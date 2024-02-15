using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Phase2Control : MonoBehaviour
{
    public int lifes = 3;
    public GameObject[] lifesGraphics;
    public GameObject[] doors;
    public GameObject[] rooms;
    public GameObject gameOverWindow;
    public GameObject gameOverInfo;

    public GameObject player, interaction, gunSystem, gunRegulation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameOver();
        }
    }

    public void LoseLife()
    {
        lifes--;
        if (lifes == 0)
        {
            GameOver();
        }

        GameObject lifeTolose = lifesGraphics[lifes];
        Animator animator = lifeTolose.GetComponent<Animator>();
        animator.SetTrigger("lose");
    }

    private void GameOver()
    {
        StopGame();
        DisplayGameInfo();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void StopGame()
    {
        player.GetComponent<FPSController>().enabled = false;
        interaction.GetComponent<Interaction>().enabled = false;
        gunSystem.GetComponent<GunSystem>().enabled = false;
        gunRegulation.GetComponent<GunRegulation>().enabled = false;
    }

    private void DisplayGameInfo()
    {
        var info = Results() + Mistakes();

        gameOverInfo.GetComponent<Text>().text = info;
        gameOverWindow.SetActive(true);
    }

    private string Results()
    {
        return $"Czas dzia³ania [mm:ss]: {GameTime()}" +
            $"\nLiczba poprawnie zneutralizowanych drzwi: {CorrectlyNeutralizedDoors()}" +
            $"\nLiczba poprawnie zneutralizowanych pomieszczeñ: {NeutralizedRooms()}";
    }

    private string GameTime()
    {
        TimeSpan gameTimeSpan = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
        return gameTimeSpan.ToString(@"m\:ss");
    }

    private int CorrectlyNeutralizedDoors()
    {
        return doors.Count(door => door.GetComponent<Door>().IsDoorCorrectlyNeutralized());
    }

    private int NeutralizedRooms()
    {
        return rooms.Count(room => room.GetComponent<Room>().IsRoomNeutralized());
    }

    private string Mistakes()
    {
        var missingMeasurements = doors.Count(door => door.GetComponent<Door>().hasBeenMeasuredAfterNeutralization == false);
        var doorsTooStronglyShot = doors.Count(door => door.GetComponent<Door>().tooStronglyShot);
        var doorsTooWeaklyShot = doors.Count(door => door.GetComponent<Door>().tooWeaklyShot);
        var wrongGaugeSetting = rooms.Sum(room => room.GetComponent<Room>().WallsShotWithWrongGaugeSetting());
        var omittedWalls = rooms.Sum(room => room.GetComponent<Room>().OmittedWalls());
        var wallsShotTooManyTimes = rooms.Sum(room => room.GetComponent<Room>().WallsShotTooManyTimes());

        var mistakes = "";
        mistakes += missingMeasurements > 0 ? $"\n- Brak wykonania pomiaru: {missingMeasurements}" : "";
        mistakes += doorsTooStronglyShot > 0 ? $"\n- Za du¿e ustawienie pierœcienia podczas neutralizacji drzwi: {doorsTooStronglyShot}" : "";
        mistakes += doorsTooWeaklyShot > 0 ? $"\n- Za ma³e ustawienie pierœcienia podczas neutralizacji drzwi: {doorsTooWeaklyShot}" : "";
        mistakes += wrongGaugeSetting > 0 ? $"\n- Z³e ustawienie pierœcienia podczas strzelania w pomieszczeniu za drzwiami: {wrongGaugeSetting}" : "";
        mistakes += omittedWalls > 0 ? $"\n- Pominiête œciany w pomieszczeniu za drzwiami: {omittedWalls}" : "";
        mistakes += wallsShotTooManyTimes > 0 ? $"\n- Œciany w pomieszczeniu za drzwiami trafione wiêcej ni¿ raz: {wallsShotTooManyTimes}" : "";

        return mistakes == "" ? mistakes : "\nB³êdy:" + mistakes;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
