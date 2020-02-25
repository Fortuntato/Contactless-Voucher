package com.example.nfc_tutorial;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.NfcEvent;
import android.os.Build;
import android.os.Bundle;
import android.os.Parcelable;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.io.Console;

public class MainActivity extends AppCompatActivity {

    private static String TAG = "NFCDemo:" +MainActivity.class.getSimpleName();



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        NfcAdapter nfcAdapter = NfcAdapter.getDefaultAdapter(this);

        if (nfcAdapter != null && nfcAdapter.isEnabled()) {
            Toast.makeText(this, "NFC is ENABLED!", Toast.LENGTH_LONG).show();
        }
    }

    NfcAdapter mNfcAdapter;

    private void processNFCData( Intent inputIntent ) {
        TextView view1 = (TextView) findViewById(R.id.textView1);
        Parcelable[] rawMessages =
                inputIntent.getParcelableArrayExtra(NfcAdapter.EXTRA_NDEF_MESSAGES);

        if (rawMessages != null && rawMessages.length > 0) {

            NdefMessage[] messages = new NdefMessage[rawMessages.length];

            for (int i = 0; i < rawMessages.length; i++) {

                messages[i] = (NdefMessage) rawMessages[i];

            }
            view1.setText("Message size = " + messages.length);
            // only one message sent during the Android beam
            // so you can just grab the first record.
            NdefMessage msg = (NdefMessage) rawMessages[0];

            // record 0 contains the MIME type, record 1 is the AAR, if present
            String payloadStringData = new String(msg.getRecords()[0].getPayload());

            // now do something with your payload payloadStringData

        }
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);

        if (intent != null && NfcAdapter.ACTION_NDEF_DISCOVERED.equals( intent.getAction() )) {
            // We scanned an NFC Tag.
            processNFCData( intent );
            Toast.makeText(this, "NFC FOUND!", Toast.LENGTH_LONG).show();
        }

    }

    @Override
    protected void onResume() {
        super.onResume();

        // Check to see that the Activity started due to an Android Beam
        if (NfcAdapter.ACTION_NDEF_DISCOVERED.equals( getIntent().getAction() )) {
            // Yes, Activity start via Beam...  wonder if we should pass a flag indicating Beam?
            processNFCData( getIntent() );
        }
    }


    private void setDisplayText ( String text ) {

        TextView view = findViewById(R.id.viewdata);
        if ( view != null ) {
            view.setText(text);
        }
    }


    private View.OnClickListener _onBeamClick = new View.OnClickListener() {
        @Override
        public void onClick(View inputView) {
            Log.i(TAG, "_onBeamClick onClick");
            turnOnNfcBeam();
        }
    };


    /* **************************************************************
        This will create the NFC Adapter, if available,
        and setup the Callback listener when create message is needed.
     */
    private void turnOnNfcBeam() {
        // Check for available NFC Adapter
        if ( mNfcAdapter == null ) {
            mNfcAdapter = NfcAdapter.getDefaultAdapter(this);
        }
        if (mNfcAdapter == null || !mNfcAdapter.isEnabled()) {
            mNfcAdapter = null;
            Toast.makeText(this, "NFC is not available", Toast.LENGTH_LONG).show();
            return;
        }

        // Register callback
        mNfcAdapter.setNdefPushMessageCallback(_onNfcCreateCallback, this);
    }

    private NfcAdapter.CreateNdefMessageCallback _onNfcCreateCallback = new NfcAdapter.CreateNdefMessageCallback() {
        @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
        @Override
        public NdefMessage createNdefMessage(NfcEvent inputNfcEvent) {
            Log.i(TAG, "createNdefMessage");
            return createMessage();
        }
    };

    @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
    private NdefMessage createMessage() {
        String text = ("Hello there from another device!\n\n" +
                "Beam Time: " + System.currentTimeMillis());
        NdefMessage msg = new NdefMessage(
                new NdefRecord[] { NdefRecord.createMime(
                        "application/com.bluefletch.nfcdemo.mimetype", text.getBytes())
                        /**
                         * The Android Application Record (AAR) is commented out. When a device
                         * receives a push with an AAR in it, the application specified in the AAR
                         * is guaranteed to run. The AAR overrides the tag dispatch system.
                         * You can add it back in to guarantee that this
                         * activity starts when receiving a beamed message. For now, this code
                         * uses the tag dispatch system.
                        */
                        //,NdefRecord.createApplicationRecord("com.example.android.beam")
                });
        return msg;
    }

}
