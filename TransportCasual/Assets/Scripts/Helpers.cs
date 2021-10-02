using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Helpers
{
    public static RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance,
        int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);

        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        Debug.DrawLine(origen, origen + new Vector2(.1f, .1f), Color.magenta);

        return hit;
    }

    public static void RoundFloat(ref float number)
    {
        number = (float) Math.Round(number, 2);
    }

    public static bool DidItProc(float chance)
    {
        var number = Random.Range(0, 100);
        return number < chance;
    }

    public static int CalculatePercent(this int number, int percent)
    {
        return (int) (number * (percent / 100f));
    }

    public static float CalculatePercentFloat(this float number, float percent)
    {
        return number * (percent / 100);
    }

    public static bool RateLimiter(int frequency)
    {
        return Time.frameCount % frequency == 0;
    }

    public static bool IsBetween(this float value, float min, float max)
    {
        return min < value && value < max;
    }

    public static bool IsBetween(this int value, int min, int max)
    {
        return min < value && value < max;
    }

    public static int RoundToNearest5(this int i)
    {
        return (int) (Math.Ceiling(i / 5.0d) * 5);
    }

    public static int RoundToNearest5(this float i)
    {
        return (int) (Math.Ceiling(i / 5.0d) * 5);
    }

    public static float RoundDigits(this float num, int digits)
    {
        return (float) Math.Round(num, digits);
    }

    public static string ToRoman(this int num)
    {
        return num switch
        {
            1 => "I",
            2 => "II",
            3 => "III",
            4 => "IV",
            5 => "V",
            _ => "",
        };
    }

    public static void SetVelocity(this Rigidbody2D rb2d, float velX, float velY)
    {
        var newVel = new Vector2(velX, velY);
        rb2d.velocity = newVel;
    }

    public static string EncryptDecrypt(string inputString)
    {
        // Define XOR key
        // Any character value will work
        char xorKey = 'S';

        // Define String to store encrypted/decrypted String
        String outputString = "";

        // calculate length of input string
        int len = inputString.Length;

        // perform XOR operation of key
        // with every caracter in string
        for (int i = 0; i < len; i++)
        {
            outputString += char.ToString((char) (inputString[i] ^ xorKey));
        }

        return outputString;
    }
}