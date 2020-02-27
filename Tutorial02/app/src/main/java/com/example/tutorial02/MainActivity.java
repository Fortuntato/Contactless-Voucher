package com.example.tutorial02;

import androidx.appcompat.app.AppCompatActivity;

import android.app.PendingIntent;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.nfc.tech.Ndef;
import android.nfc.tech.NdefFormatable;
import android.nfc.tech.NfcA;
import android.os.Bundle;
import android.util.Log;
import android.widget.CompoundButton;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.io.ByteArrayOutputStream;
import java.io.UnsupportedEncodingException;
import java.util.Locale;

public class MainActivity extends AppCompatActivity {

    NfcAdapter nfcAdapter;

    Switch switchReadWrite;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Get the instance of the switch
        switchReadWrite = findViewById(R.id.readWriteSwitch);
        switchReadWrite.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (isChecked){
                    // Toggle is enabled. Writing mode

                }else{
                    // Toggle is disabled. Reading mode

                }

            }
        });

        //Try to get the adapter of the Nfc
        nfcAdapter = NfcAdapter.getDefaultAdapter(this);
        if(nfcAdapter != null && nfcAdapter.isEnabled()){
            Toast.makeText(this, "NFC is enabled", Toast.LENGTH_LONG).show();
        }else{
            Toast.makeText(this, "NFC not enabled :((((", Toast.LENGTH_LONG).show();
            finish();
        }
    }

    @Override
    protected void onNewIntent(Intent intent) {
        Toast.makeText(this,"Nfc intent received", Toast.LENGTH_SHORT).show();
        TextView textview = findViewById(R.id.readTextView);

        super.onNewIntent(intent);

        if(intent.hasExtra(NfcAdapter.EXTRA_TAG)){
            Toast.makeText(this,"Nfc extra tag intent", Toast.LENGTH_SHORT).show();

            Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);

            TextView userTextViewEdit = findViewById(R.id.editText);
            NdefMessage ndefMessage = createNdefMessage(userTextViewEdit.toString());
            writeNdefMessage(tag, ndefMessage);
        }

    }

    @Override
    protected void onResume() {
        Intent intent = new Intent(this, MainActivity.class);
        intent.addFlags(Intent.FLAG_RECEIVER_REPLACE_PENDING);

        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0 ,intent,0);
        IntentFilter[] intentfilter = new IntentFilter[]{};

        nfcAdapter.enableForegroundDispatch(this, pendingIntent, intentfilter, null);

        super.onResume();
    }

    @Override
    protected void onPause() {
        nfcAdapter.disableForegroundDispatch(this);
        super.onPause();
    }

    private void enableForegroundDispatchSystem(){
        Intent intent = new Intent(this, MainActivity.class).addFlags(Intent.FLAG_RECEIVER_REPLACE_PENDING);

        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0 ,intent, 0);

        IntentFilter[]intentFilters = new IntentFilter[]{};
        nfcAdapter.enableForegroundDispatch(this,pendingIntent, intentFilters, null);

    }

    private void disableForegroundDispatchSystem(){
        nfcAdapter.disableForegroundDispatch(this);
    }

    private void formatTag(Tag tag, NdefMessage ndefMessage){
        try{

            NdefFormatable ndefFormatable = NdefFormatable.get(tag);

            if (ndefFormatable==null){
                Toast.makeText(this,"Tag is not NDEF formatable", Toast.LENGTH_SHORT).show();
            }

            ndefFormatable.connect();
            ndefFormatable.format(ndefMessage);
            ndefFormatable.close();

            Toast.makeText(this, "Tag write finished", Toast.LENGTH_SHORT).show();

        }catch(Exception e){
            Log.e("formatTag", e.getMessage());
        }
    }

    private void writeNdefMessage(Tag tag, NdefMessage ndefMessage){
        try {

            if (tag ==null){
                Toast.makeText(this,"Tag object is null", Toast.LENGTH_SHORT).show();
                return;
            }

            Ndef ndef = Ndef.get(tag);

            if (ndef == null){
                // format tag with the ndef format and writes the mssage
                formatTag(tag,ndefMessage);
            }else{
                ndef.connect();

                if (!ndef.isWritable()){
                    Toast.makeText(this, "Tag is not writable...", Toast.LENGTH_SHORT).show();
                    ndef.close();
                    return;
                }

                ndef.writeNdefMessage(ndefMessage);
                ndef.close();

                Toast.makeText(this,"Tag written successfully", Toast.LENGTH_LONG).show();

            }


        }catch (Exception e){
            Log.e("writeNdefMessage", e.getMessage());
        }
    }

    private NdefRecord createTextRecord(String content) {
        try {
            byte[] language;
            language = Locale.getDefault().getLanguage().getBytes("UTF-8");

            final byte[] text = content.getBytes("UTF-8");
            final int languageSize = language.length;
            final int textLength = text.length;
            final ByteArrayOutputStream payload = new ByteArrayOutputStream(1 + languageSize + textLength);

            payload.write((byte) (languageSize & 0x1F));
            payload.write(language, 0, languageSize);
            payload.write(text, 0, textLength);

            return new NdefRecord(NdefRecord.TNF_WELL_KNOWN, NdefRecord.RTD_TEXT, new byte[0], payload.toByteArray());

        } catch (Exception e) {
            Log.e("CreateTextRecord", e.getMessage());
        }
        return null;
    }

    private NdefMessage createNdefMessage (String content){

        NdefRecord ndefRecord = createTextRecord(content);
        NdefMessage ndefMessage = new NdefMessage(new NdefRecord[] { ndefRecord } );

        return ndefMessage;
    }

}
