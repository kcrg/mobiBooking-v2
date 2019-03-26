import 'package:flutter/material.dart';
import './my_meetings.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class Dashboard extends StatefulWidget {
  _DashboardState createState() => _DashboardState();
}

class _DashboardState extends State<Dashboard> {
  Future<void> fakeRequest() async {
    return Future.delayed(Duration(seconds: 1), () {

    });
  }
  Widget _getChild(index) {
    switch (index) {
      case 0:
        return MyMeetings();
        break;
      default:
        return null;
    }
  }

  @override
  Widget build(BuildContext context) {
    return LiquidPullToRefresh(
      showChildOpacityTransition: false,
      onRefresh: fakeRequest,
      color: Theme.of(context).accentColor,
      child: new ListView.builder(
        itemCount: 6,
        itemBuilder: (context, index) {
          return Container(
              margin: EdgeInsets.only(left: 20, right: 20, top: 10, bottom: 10),
              height: 235,
              decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.all(Radius.circular(5)),
                  boxShadow: <BoxShadow>[
                    BoxShadow(
                      color: Colors.grey,
                      offset: Offset(1, 1),
                      blurRadius: 5.0,
                    ),
                  ]),
              child: _getChild(index));
        },
      ),
    );
  }
}
