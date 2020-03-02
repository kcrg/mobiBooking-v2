import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

class TodayMeetings extends StatefulWidget {
  @override
  _TodayMeetingsState createState() => _TodayMeetingsState();
}

class _TodayMeetingsState extends State<TodayMeetings> {
  bool waitForResponse = true;

  List<dynamic> meetings = new List();

  
  void initState() {
    super.initState();
    _getTodayMeetings();
  }

  Future _getTodayMeetings() async {
    SharedPreferences.getInstance().then((value) {
      String token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Meetings/today",
        headers: {
          "Content-Type": "application/json",
          "Authorization": "bearer " + token
        },
      ).then((dynamic res) {
        setState(() {
          waitForResponse = false;
        });

        if (res.statusCode == 200) {
          setState(() {
            meetings = json.decode(res.body);
          });
        }
      });
    });
  }

  Widget _generateElement(String time, String title, String room) {
    return Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: <Widget>[
          Text(time,
              style: TextStyle(
                  color: Theme.of(context).accentColor,
                  fontSize: 21,
                  fontWeight: FontWeight.bold),
              textAlign: TextAlign.center),
          Text(title,
              style: TextStyle(
                  color: Colors.grey,
                  fontSize: 21,
                  fontWeight: FontWeight.bold),
              textAlign: TextAlign.center),
          Text(room,
              style: TextStyle(
                  color: Colors.grey,
                  fontSize: 21,
                  fontWeight: FontWeight.bold),
              textAlign: TextAlign.center),
        ]);
  }

  @override
  Widget build(BuildContext context) {
    return waitForResponse
        ? Center(
            child: SizedBox(
            child: new CircularProgressIndicator(
              strokeWidth: 5,
            ),
            height: 50.0,
            width: 50.0,
          ))
        : Column(
            children: <Widget>[
              Align(
                alignment: Alignment.topLeft,
                child: Container(
                    margin: EdgeInsets.only(left: 15, top: 15),
                    child: Text(
                      "Spotkania na dziś",
                      style: TextStyle(fontSize: 30, color: Colors.grey),
                    )),
              ),
              Expanded(
                  child: ListView.builder(
                itemCount: meetings.length,
                itemBuilder: (item, index) {
                  return Container(
                      margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                      child: _generateElement(meetings[index]["time"],
                          meetings[index]["title"], meetings[index]["roomName"]));
                },
              ))
            ],
          );
  }
}
