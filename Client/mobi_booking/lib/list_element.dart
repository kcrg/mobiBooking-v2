import 'package:flutter/material.dart';

class ListElement extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
          color: Color(0xff002132), borderRadius: new BorderRadius.circular(5)),
      padding: const EdgeInsets.all(8.0),
      alignment: Alignment.topLeft,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Padding(
            padding: const EdgeInsets.all(5.0),
            child: Text(
                '3/22 PBE UPDATE: NEW SUMMONER ICONS, LEC LOGIN, & MORE',
                style: TextStyle(
                    color: Color(0xffce9f3d),
                    fontSize: 20,
                    fontWeight: FontWeight.bold)),
          ),
          Padding(
            padding: const EdgeInsets.all(5.0),
            child: Text('Posted on March 22, 2019 at 1:15 PM by Aznbeat',
                style: TextStyle(color: Colors.white, fontSize: 13)),
          ),
          Padding(
              padding: const EdgeInsets.all(5.0),
              child: Text(
                  "The PBE has been updated! As we continue the 9.7 PBE cycle, "
                  "today's patch includes new summoner icons, a login theme for the LEC 2019 "
                  "Spring Split, and more!",
                  style: TextStyle(color: Colors.white, fontSize: 15))),
        ],
      ),
      margin: EdgeInsets.fromLTRB(10, 5, 10, 5),
    );
  }
}
