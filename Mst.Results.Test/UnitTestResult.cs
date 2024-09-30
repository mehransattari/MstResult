using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Xunit.Sdk;

namespace Mst.Results.Test;

public class UnitTestResult
{
    #region AddErrorMessage

    [Fact]
    public void AddErrorMessage_Should_Add_Fixed_message()
    {
        //Arrange
        var result = new Mst.Results.Result();
        string errorMessage = "   Spaces around    ";
        string fixedMessage = "Spaces around";

        //Act
        result.AddErrorMessage(errorMessage);

        //Assert
        Assert.Equal(fixedMessage, result.Errors.First());
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void AddErrorMessage_Should_Add_Message_When_Is_Valid()
    {
        //Arrange
        var result = new Mst.Results.Result();
        var errorMessage = "Error Message";

        //Act
        result.AddErrorMessage(errorMessage);

        //Assert
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void AddErrorMessage_Should_Not_Add_Empty_Message()
    {
        //Arrange
        var result = new Mst.Results.Result();
        var errorMessage = string.Empty;

        //Act
        result.AddErrorMessage(errorMessage);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailed);
    }

    [Fact]
    public void AddErrorMessage_Should_Not_Add_Null_Message()
    {
        //Arrange
        var result = new Mst.Results.Result();

        //Act
        result.AddErrorMessage(null);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailed);
    }

    [Fact]
    public void AddErrorMessage_Should_Not_Add_Duplicate_Message()
    {
        //Arrange
        var result = new Mst.Results.Result();
        var errorMessage = "Duplicate Message";

        //Act
        result.AddErrorMessage(errorMessage);
        result.AddErrorMessage(errorMessage);

        //Assert
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
    }

    #endregion

    #region RemoveErrorMessage

    [Fact]
    public void RemoveErrorMessage_Should_Not_Remove_Empty_Message()
    {
        //Arrange
        var result = new Mst.Results.Result();
        result.AddErrorMessage("Error 1");

        //Act
        result.RemoveErrorMessage("");

        //Assert
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void RemoveErrorMessage_Should_Not_Remove_Null_Message()
    {
        //Arrange
        var result = new Mst.Results.Result();
        result.AddErrorMessage("Error 1");

        //Act
        result.RemoveErrorMessage(null);

        //Assert
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void RemoveErrorMessage_Should_Remove_Message_And_Update_Status()
    {
        //Arrange
        var result = new Mst.Results.Result();
        string message = "Error 1";
        result.AddErrorMessage(message);

        //Act
        result.RemoveErrorMessage(message);

        //Assert
        Assert.False(result.IsFailed);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void RemoveErrorMessage_Should_Not_Affect_Status_When_Removing_Non_Existing_Message()
    {
        // Arrange  
        var result = new Result();
        result.AddErrorMessage("Error 1");

        // Act  
        result.RemoveErrorMessage("Non-existing error"); // Trying to remove a non-existing error  

        // Assert  
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void RemoveErrorMessage_Should_Remove_Existing_Message_And_Keep_Status()
    {
        // Arrange  
        var result = new Result();
        result.AddErrorMessage("Error 1");
        result.AddErrorMessage("Error 2");

        // Act  
        result.RemoveErrorMessage("Error 1"); // Remove one of the existing messages  

        // Assert  
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Contains("Error 2", result.Errors);
    }
    #endregion

    #region ClearErrorMessages
    [Fact]
    public void ClearErrorMessages_Should_Clear_Existing_Messages()
    {
        // Arrange  
        var result = new Result();
        result.AddErrorMessage("Error 1");
        result.AddErrorMessage("Error 2");

        // Act  
        result.ClearErrorMessages();

        // Assert  
        Assert.False(result.IsFailed);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors); // Should be empty after clearing  
    }

    [Fact]
    public void ClearErrorMessages_Should_Work_When_No_Messages_Exist()
    {
        // Arrange  
        var result = new Result();

        // Act  
        result.ClearErrorMessages(); // Clear when no messages exist  

        // Assert  
        Assert.False(result.IsFailed);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors); // Should still be empty  
    }

    [Fact]
    public void ClearErrorMessages_Should_Reset_Status()
    {
        // Arrange  
        var result = new Result();
        result.AddErrorMessage("Error 1");

        // Act  
        result.ClearErrorMessages(); // Clear the messages  

        // Assert  
        Assert.False(result.IsFailed);
        Assert.True(result.IsSuccess);
    }
    #endregion

    #region AddSuccessMessage
    [Fact]
    public void AddSuccessMessage_Should_Valid_Message()
    {
        //Arrange
        var result = new Result();
        string message = "Success 1";

        //Act
        result.AddSuccessMessage(message);

        //Assert
        Assert.Single(result.Successes);
        Assert.Contains(message, result.Successes);
    }

    [Fact]
    public void AddSuccessMessage_Should_Not_Add_Null_Message()
    {
        //Arrange
        var result = new Result();

        //Act
        result.AddSuccessMessage(null);

        //Assert
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void AddSuccessMessage_Should_Not_Add_Empty_Message()
    {
        //Arrange
        var result = new Result();

        //Act
        result.AddSuccessMessage("");

        //Assert
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void AddSuccessMessage_Should_Not_Duplicate_Message()
    {
        //Arrange
        var result = new Result();
        string message = "message 1";
        result.AddSuccessMessage(message);

        //Act
        result.AddSuccessMessage(message);

        //Assert
        Assert.Single(result.Successes);
    }


    [Fact]
    public void AddSuccessMessage_Should_Add_Unique_Message()
    {
        //Arrange
        var result = new Result();
        string message1 = "message 1";
        string message2 = "message 2";

        result.AddSuccessMessage(message1);

        //Act
        result.AddSuccessMessage(message2);

        //Assert
        Assert.Equal(2, result.Successes.Count);
        Assert.Contains(message1, result.Successes);
        Assert.Contains(message2, result.Successes);
    }
    #endregion

    #region RemoveSuccessMessage

    [Fact]
    public void RemoveSuccessMessage_Should_Remove_Existing_Message()
    {
        // Arrange  
        var result = new Result();
        result.AddSuccessMessage("Success 1");
        result.AddSuccessMessage("Success 2");

        // Act  
        result.RemoveSuccessMessage("Success 1");

        // Assert  
        Assert.Single(result.Successes); // Should only have one message left  
        Assert.Contains("Success 2", result.Successes);
        Assert.DoesNotContain("Success 1", result.Successes);
    }

    [Fact]
    public void RemoveSuccessMessage_Should_Not_Affect_Non_Existing_Message()
    {
        // Arrange  
        var result = new Result();
        result.AddSuccessMessage("Success 1");

        // Act  
        result.RemoveSuccessMessage("Success 2"); // Attempt to remove a message that doesn't exist  

        // Assert  
        Assert.Single(result.Successes); // Should still have only one message  
        Assert.Contains("Success 1", result.Successes);
    }

    [Fact]
    public void RemoveSuccessMessage_Should_Not_Remove_When_Null_Message()
    {
        // Arrange  
        var result = new Result();
        result.AddSuccessMessage("Success 1");

        // Act  
        result.RemoveSuccessMessage(null); // Attempt to remove a null message  

        // Assert  
        Assert.Single(result.Successes); // Should still have only one message  
        Assert.Contains("Success 1", result.Successes);
    }

    [Fact]
    public void RemoveSuccessMessage_Should_Not_Remove_When_Empty_Message()
    {
        // Arrange  
        var result = new Result();
        result.AddSuccessMessage("Success 1");

        // Act  
        result.RemoveSuccessMessage(""); // Attempt to remove an empty message  

        // Assert  
        Assert.Single(result.Successes); // Should still have only one message  
        Assert.Contains("Success 1", result.Successes);
    }
    #endregion

    #region ClearSuccessMessages
    [Fact]
    public void ClearSuccessMessages_Should_Empty_Success_List()
    {
        // Arrange  
        var result = new Result();
        result.AddSuccessMessage("Success 1");
        result.AddSuccessMessage("Success 2");

        // Act  
        result.ClearSuccessMessages();

        // Assert  
        Assert.Empty(result.Successes); // The success messages list should be empty  
    }

    [Fact]
    public void ClearSuccessMessages_Should_Not_Throw_Error_When_Already_Empty()
    {
        // Arrange  
        var result = new Result(); // List starts empty  

        // Act  
        result.ClearSuccessMessages(); // Call clear on an already empty list  

        // Assert  
        Assert.Empty(result.Successes); // Should remain empty without error  
    }
    #endregion

    #region ClearAllMessages
    [Fact]
    public void ClearAllMessages_Should_Empty_Both_Error_and_Success_Lists()
    {
        // Arrange  
        var result = new Result();
        result.AddSuccessMessage("Success 1");
        result.AddSuccessMessage("Success 2");
        result.AddErrorMessage("Error 1");
        result.AddErrorMessage("Error 2");

        // Act  
        result.ClearAllMessages();

        // Assert  
        Assert.Empty(result.Successes); // The success messages list should be empty  
        Assert.Empty(result.Errors);   // The error messages list should be empty  
    }

    [Fact]
    public void ClearAllMessages_Should_Not_Throw_Error_When_Already_Empty()
    {
        // Arrange  
        var result = new Result(); // Both lists start empty  

        // Act  
        result.ClearAllMessages(); // Call clear on empty lists  

        // Assert  
        Assert.Empty(result.Successes); // Should remain empty without error  
        Assert.Empty(result.Errors);   // Should also remain empty without error  
    }
    #endregion
}