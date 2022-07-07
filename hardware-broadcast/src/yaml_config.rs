pub mod yaml_configuration {
    use std::fs::File;
    use std::io::{Write, Error, Read};
    use std::path::Path;

    use yaml_rust::{YamlLoader};
    extern crate yaml_rust;

    const DEFAULT_CONFIG_PATH: &str = "./hardware-broadcast-config.yaml";

    pub struct KafkaDomainConfigModel {
        pub server: String,
        pub port: String,
        pub topic: String
    }
    impl KafkaDomainConfigModel {
        pub fn new(server: String, port: String, topic: String) -> KafkaDomainConfigModel {
            KafkaDomainConfigModel { server: server, port: port, topic: topic }
        }
    }
    pub struct YamlConfigModel {
        pub kafkaConfig: KafkaDomainConfigModel
    }
    impl YamlConfigModel {
        pub fn new(kafkaConfig: KafkaDomainConfigModel) -> YamlConfigModel {
            YamlConfigModel { kafkaConfig: kafkaConfig }
        }
    }

    pub fn init_configs() -> Result<YamlConfigModel, Error> {
        let file_exist = Path::new(DEFAULT_CONFIG_PATH).is_file();
        let mut yaml_file = if file_exist { File::open(DEFAULT_CONFIG_PATH)? } 
            else { File::create(DEFAULT_CONFIG_PATH)? };

        let mut file_text_buffer = String::new();
        if file_exist {
            yaml_file.read_to_string(&mut file_text_buffer).expect("A error occurs.");
        }
        else {
            file_text_buffer = create_default_config_model();
            write!(&mut yaml_file, "{}", file_text_buffer)?;
        }
        let kafka_config: KafkaDomainConfigModel = load_from_yaml(file_text_buffer);
        let final_config: YamlConfigModel = YamlConfigModel::new(kafka_config);

        Ok(final_config)
    }

    fn load_from_yaml(text: String) -> KafkaDomainConfigModel {
        let yaml_params = YamlLoader::load_from_str(text.as_str()).expect("Invalid yaml.");
        let yaml_params = &yaml_params[0];
        
        let server = yaml_params["kafka_domain_configuration"]["server"].as_str().unwrap();
        let port = yaml_params["kafka_domain_configuration"]["port"].as_str().unwrap();
        let topic = yaml_params["kafka_domain_configuration"]["topic"].as_str().unwrap();
        
        KafkaDomainConfigModel::new(String::from(server), String::from(port), String::from(topic))
    }
    fn create_default_config_model() -> String {
        let mut config_model = String::new();
        config_model.push_str("kafka_domain_configuration:\n");
        config_model.push_str("  server: localhost\n");
        config_model.push_str("  port: 9092\n");
        config_model.push_str("  topic: hardware_streaming\n");
        

        config_model
    }
}