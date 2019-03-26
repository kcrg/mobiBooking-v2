import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import './User.dart';

class LoginPage extends StatefulWidget {
  final void Function(String) _loginCallback;

  LoginPage(this._loginCallback);

  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  String _urlBase = 'https://mobibookingwebapi.azurewebsites.net/api/';

  final emailController = TextEditingController();

  final passwordController = TextEditingController();

  _login() async {
    var map = new Map<String, dynamic>();
    map["email"] = emailController.text;
    map["password"] = passwordController.text;

    await http.post(
      "https://mobibookingwebapi.azurewebsites.net/api/Account/login",
      body: json.encode(map),
      headers: {"Content-Type": "application/json"},
    ).then((dynamic res) {
      if (res.statusCode == 200){
        TokenModel tokenModel = TokenModel.fromJson(jsonDecode(res.body));     
        widget._loginCallback(tokenModel.token);
      }

    });
  }

  @override
  Widget build(BuildContext context) {
    return new Center(
      child: Container(
        margin: EdgeInsets.only(left: 80, right: 80),
        alignment: Alignment.center,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            TextField(
              keyboardType: TextInputType.emailAddress,
              controller: emailController,
              decoration: InputDecoration(hintText: 'Podaj adres email'),
            ),
            TextField(
              obscureText: true,
              controller: passwordController,
              decoration: InputDecoration(hintText: 'Podaj hasło'),
            ),
            Container(
              margin: EdgeInsets.only(top: 20),
              child: RaisedButton(
                onPressed: _login,
                child: Text("Zaloguj"),
              ),
            )
          ],
        ),
      ),
    );
  }
}
