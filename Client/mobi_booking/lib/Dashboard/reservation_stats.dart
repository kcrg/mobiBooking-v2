import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class ReservationStats extends StatefulWidget {
  @override
  _ReservationStatsState createState() => _ReservationStatsState();
}

class _ReservationStatsState extends State<ReservationStats> {
  String token;
  bool waitForResponse = true;
  String thisWeek = "0";
  String lastWeek = "0";
  String thisMonth = "0";
  String lastMonth = "0";

  void initState() {
    super.initState();
    _getThisWeek();
    _getLastMonth();
    _getThisMonth();
    _getLastWeek();
  }

  Future _getThisWeek() async {
    SharedPreferences.getInstance().then((value) {
      token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Meetings/count_this_week",
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
            thisWeek = res.body;
          });
        }
      });
    });
  }

  Future _getLastWeek() async {
    SharedPreferences.getInstance().then((value) {
      token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Meetings/count_last_week",
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
            lastWeek = res.body;
          });
        }
      });
    });
  }

  Future _getThisMonth() async {
    SharedPreferences.getInstance().then((value) {
      token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Meetings/count_this_month",
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
            thisMonth = res.body;
          });
        }
      });
    });
  }

  Future _getLastMonth() async {
    SharedPreferences.getInstance().then((value) {
      token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Meetings/count_last_month",
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
            lastMonth = res.body;
          });
        }
      });
    });
  }

  Widget _generateColumn(bool left) {
    return Align(
      alignment: Alignment.centerLeft,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Container(
              margin: EdgeInsets.only(left: 15, top: 30),
              alignment: Alignment.centerLeft,
              child: Text(
                left ? "Ten tydzień" : "poprz. tydzień",
                style: TextStyle(fontSize: 20, color: Colors.grey),
                textAlign: TextAlign.left,
              )),
          Container(
              alignment: Alignment.centerLeft,
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                left ? thisWeek : lastWeek,
                style: TextStyle(
                    fontSize: 30,
                    color: Color(0xff902fdb),
                    fontWeight: FontWeight.bold),
              )),
          Container(
              alignment: Alignment.centerLeft,
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                left ? "ten miesiąc" : "poprz. miesiąc",
                style: TextStyle(fontSize: 20, color: Colors.grey),
              )),
          Container(
              alignment: Alignment.centerLeft,
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                left ? thisMonth : lastMonth,
                style: TextStyle(
                    fontSize: 30,
                    color: Color(0xff902fdb),
                    fontWeight: FontWeight.bold),
              )),
        ],
      ),
    );
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
                      "Statystyki rezerwacji",
                      style: TextStyle(fontSize: 30, color: Colors.grey),
                    )),
              ),
              Row(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: <Widget>[
                  Container(
                      margin: EdgeInsets.only(left: 0),
                      child: _generateColumn(true)),
                  Container(
                      margin: EdgeInsets.only(left: 100),
                      child: _generateColumn(false))
                ],
              )
            ],
          );
  }
}
