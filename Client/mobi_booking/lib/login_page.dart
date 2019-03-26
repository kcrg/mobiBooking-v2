import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import './token_model.dart';
import './CustomInputs/custom_input.dart';

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
      if (res.statusCode == 200) {
        TokenModel tokenModel = TokenModel.fromJson(jsonDecode(res.body));
        widget._loginCallback(tokenModel.token);
      }
    });
  }

  Widget _buildLoginForm() {
    return Column(children: <Widget>[
      Container(
          margin: EdgeInsets.only(bottom: 20),
          child: CustomInput(
            controller: emailController,
            text: "Podaj email",
          )),
      CustomInput(controller: passwordController, text: 'Podaj hasło'),
      Container(
          margin: EdgeInsets.only(top: 20),
          child: Center(
            child: new RaisedButton(
              padding: const EdgeInsets.all(15.0),
              textColor: Colors.white,
              color: Theme.of(context).primaryColor,
              onPressed: _login,
              child: new Text(
                "Zaloguj",
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
              ),
            ),
          ))
    ]);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      mainAxisSize: MainAxisSize.max,    
      children: <Widget>[
        Container(
            alignment: Alignment.topCenter,
            margin: EdgeInsets.only(top: 100, left: 50, right: 50),

            child: Image.asset(
              'assets/logo.png',
            )),
        Container(
          margin: EdgeInsets.only(top: 100, left: 50, right: 50),
          alignment: Alignment.bottomCenter,
          child: _buildLoginForm())
      ],
    );
  }
}
