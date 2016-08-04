function PlayAudio(s1, s2) {
    myPlayer.stop;
    myPlayer.setInfo(s2);
    s3 = 'http://ho-server-01.cloudapp.net/m/' + s1 + s2 + ".mp3";
    myPlayer.play(s3);
}