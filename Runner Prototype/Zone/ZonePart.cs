using UnityEngine;
using TMPro;
using System;

public class ZonePart : MonoBehaviour
{
    [SerializeField] private TMP_Text _expressionText;
    [SerializeField] private int _partZoneNumber;
    [SerializeField] private int _maxValueForExpression;
    [SerializeField] private bool _isEasyMode;
    [SerializeField] private Zone _zone;

    private bool _isGood;

    public string Expression { private set; get; }
    public float Value { private set; get; }

    private void Start()
    {
        _isGood = DetermineGoodOrNot();
        Value = GenerateValueForExpression();
        int expressionID = GenerateExpressionID();

        switch (_isEasyMode) 
        {
            case true:
                Expression = ChooseExpressionEasy(expressionID);
                break;
            case false:
                Expression = ChooseExpressionHard(expressionID);
                break;
        }

        TryToRecaculateValue(expressionID);
        _expressionText.text = Expression + Value.ToString();
    }

    private void OnTriggerEnter(Collider body)
    {
        if (body.TryGetComponent<Squad>(out Squad squad))
        {
            squad.ChangePointsValue(Expression, Value, _isGood);
        }
    }

    private bool DetermineGoodOrNot() 
    {
        if (_partZoneNumber == _zone.GoodZoneNumber)
            return true;
        else
            return false;
    }

    private int GenerateExpressionID() 
    {
        int minValue, maxValue;

        switch (_isGood) 
        {
            case true:
                minValue = ConstantsContainer.Expression.EasyMode.MinGoodID;
                maxValue = ConstantsContainer.Expression.EasyMode.MaxGoodID + 1;
                break;
            case false:
                minValue = ConstantsContainer.Expression.EasyMode.MinBadID;
                maxValue = ConstantsContainer.Expression.EasyMode.MaxBadID + 1;
                break;
        }

        return _zone.ValueRandomizer.Randomizer.Next(minValue, maxValue);       
    }

    private int GenerateValueForExpression() 
    {
        return _zone.ValueRandomizer.Randomizer.Next(1, _maxValueForExpression);
    }

    private string ChooseExpressionEasy(int currentID) 
    {
        switch (currentID)
        {
            case ConstantsContainer.Expression.Good.Addition:
                return ConstantsContainer.Expression.Visualization.Addition;
            case ConstantsContainer.Expression.Good.Multiplication:
                return ConstantsContainer.Expression.Visualization.Multiplication;
            case ConstantsContainer.Expression.Good.Division:
                return ConstantsContainer.Expression.Visualization.Division;
            case ConstantsContainer.Expression.Bad.Subtraction:
                return ConstantsContainer.Expression.Visualization.Subtraction;
            case ConstantsContainer.Expression.Bad.Multiplication:
                return ConstantsContainer.Expression.Visualization.Multiplication;
            case ConstantsContainer.Expression.Bad.Division:
                return ConstantsContainer.Expression.Visualization.Division;
        }

        return "";
    }

    private string ChooseExpressionHard(int expressionID) 
    {
        switch (expressionID)
        {
            case ConstantsContainer.Expression.Good.Addition:
                return ConstantsContainer.Expression.Visualization.Addition;
            case ConstantsContainer.Expression.Good.Multiplication:
                return ConstantsContainer.Expression.Visualization.Multiplication;
            case ConstantsContainer.Expression.Good.Division:
                return ConstantsContainer.Expression.Visualization.Division;
            case ConstantsContainer.Expression.Bad.Subtraction:
                return ConstantsContainer.Expression.Visualization.Subtraction;
            case ConstantsContainer.Expression.Bad.Multiplication:
                return ConstantsContainer.Expression.Visualization.Multiplication;
            case ConstantsContainer.Expression.Bad.Division:
                return ConstantsContainer.Expression.Visualization.Division;
        }

        return "";
    }

    private void TryToRecaculateValue(int expressionID) 
    {
        switch (expressionID) 
        {           
            case ConstantsContainer.Expression.Good.Division:
                Value = (float) Math.Round(1 / Value, 1);              
                break;         
            case ConstantsContainer.Expression.Bad.Multiplication:
                Value = (float)Math.Round(1 / Value, 1);
                break;       
        }
    } 
}