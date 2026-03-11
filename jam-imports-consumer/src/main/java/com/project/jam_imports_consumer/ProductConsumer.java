package com.project.jam_imports_consumer;

import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Service;

@Service
public class ProductConsumer {

    @KafkaListener(topics =  "jam-imports-new-product", groupId = "jam-group")
    public void listenNewProducts(String message){
        System.out.println("=====================================");
        System.out.println("NOTIFICAÇÃO RECEBIDA PELO SISTEMA JAVA!");
        System.out.println("Conteúdo do produto: "+ message);
        System.out.println("=====================================");
    }
}
