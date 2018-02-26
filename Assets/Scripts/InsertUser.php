<?php
//opening script
	///VARIABLES///always start with a $
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$db_name = "login_system";
	
	$username = $_POST["username_post"];
	$password = $_POST["password_post"];
	$email = $_POST["email_post"];
	
	//Start
	//Make a Connection to our server
	$conn = new mysqli($server_name, $server_username,$server_password,$db_name);
	//Check Connection
	if(!$conn)
	{
		die("Connection Failed.".mysql_connect_error());
	}
	
	//set our username from the users table
	$checkuser = "SELECT username FROM users";
	$result = mysqli_query($conn,$checkuser);
	$canmakeaccount = "";
	//we dont have any users
	if(mysqli_num_rows($result) <= 0)
	{
		//create users
		$canmakeaccount = "creating_account";
		$createuser = "INSERT INTO users (username,email,password)VALUES('".$username."','".$email."','".$password."')";
		$resultcreate = mysqli_query($conn,$createuser);
		
		if(!$resultcreate)
		{
			echo "Error";
		}
		else
		{
			echo "Create First User";
		}
	}
	//else if we have users, check matches 
	else if(mysqli_num_rows($result)>0)
	{
		while($row = mysqli_fetch_assoc($result))
		{
			if($row['username'] == $username)
			{
				$canmakeaccount = "cant_user";
				echo "User Already Exists";
			}
			else
			{
				$canmakeaccount = "email_check";
			}
		}
	}
	else if($canmakeaccount == "email_check" && mysqli_num_rows($result) > 0)
	{
		$checkemail = "SELECT email FROM users";
		$resultemail = mysqli_query($conn,$checkemail);
		
		if(mysqli_num_rows($resultemail) > 0)
		{
			while($row = mysqli_fetch_assoc($resultemail))
			{
				if($row['email'] == $email)
				{
					$canmakeaccount = "cant_email";
					echo "Email Already Exists";
				}
				else
				{
					//create users
					$canmakeaccount = "creating_account";
					$createuser = "INSERT INTO users (username,email,password)VALUES('".$username."','".$email."','".$password."')";
					$resultcreate = mysqli_query($conn,$createuser);
		
					if(!$resultcreate)
					{
						echo "Error";
					}
					else
					{
						echo "Create User";
					}
				}
			}
		}
	}
	$canmakeaccount = "";
//closing script
?>