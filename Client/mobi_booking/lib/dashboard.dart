import 'package:flutter/material.dart';

class Dashboard extends StatefulWidget {
  _DashboardState createState() => _DashboardState();
}

class _DashboardState extends State<Dashboard> {
  @override
  Widget build(BuildContext context) {
    return new Column(
      children: <Widget>[
        Container(
          margin: EdgeInsets.all(20),
          height: 150,
          color: Colors.blue,
        ),

        Container(
          margin: EdgeInsets.all(20),
          height: 150,
          color: Colors.blue,
        ),

        Container(
          margin: EdgeInsets.all(20),
          height: 150,
          color: Colors.blue,
        ),

        Container(
          margin: EdgeInsets.all(20),
          height: 150,
          decoration: BoxDecoration(borderRadius: BorderRadius.all(Radius.circular(10))),
          color: Colors.blue,
        )
      ],
    );
  }
}
